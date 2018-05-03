using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using kms.Data.Entities;
using kms.Data;
using kms.Models;
using kms.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using kms.Utils;

namespace kms.Controllers
{
    [KmsController, Authorize]
    public class DocumentsController : Controller
    {
        private int UserId { get { return int.Parse(User.Identity.Name); } }

        private readonly KMSDBContext _db;
        public DocumentsController(KMSDBContext context)
        {
            this._db = context;
        }

        #region CRUD

        [HttpGet]
        public async Task<IActionResult> GetList([FromQuery] int? project, [FromQuery] int? offset, [FromQuery] int? limit, [FromQuery] string query) {
            if (!project.HasValue) {
                return BadRequest();
            }

            var documentsQuery = _db.Documents.Where(d => d.ProjectId == project).OrderByDescending(d => d.DateCreated);
            var count = await documentsQuery.CountAsync();
            var documents = await documentsQuery.Skip(offset.HasValue ? offset.Value : 0).Take(limit.HasValue ? limit.Value : 50).ToListAsync();
            var results = documents.Select(d => new DocumentShortDto(d));
            return Ok(new { count, results });
        }

        [HttpGet("{id:int}")]
        [HttpGet("{slug:regex([[\\w-]])}")]
        public async Task<IActionResult> GetSingle([FromRoute] int? id, [FromRoute] string slug) {
            var document = await GetDocument(id, slug);
            if (document == null) {
                return NotFound();
            }

            var documentText = await _db.DocumentText.SingleOrDefaultAsync(t => t.DocumentId == document.DocumentId && t.IsActual);
            var content = documentText == null ? "" : documentText.Content;

            return Ok(new DocumentDto(document, content));
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] DocumentCreateDto document) {
            if (document == null) {
                return BadRequest();
            }

            int attemptsCount = 0;
            int maxAttemptsCount = 100;
            string slug;
            bool isSlugUnique = false;

            do {
                slug = document.Title.GenerateSlug();
                isSlugUnique = (await _db.Documents.SingleOrDefaultAsync(d => d.Slug == slug)) == null;
            } while (!isSlugUnique && attemptsCount < maxAttemptsCount);

            if (!isSlugUnique) {
                return BadRequest(new { message = "Cannot generate unique slug" });
            }

            var newDocument = new Documents{
                ParentDocumentId = document.ParentDocumentId,
                ProjectId = document.ProjectId,
                CreatorId = UserId,
                Slug = slug,
                Title = document.Title,
                Subtitle = document.Subtitle,
                DateCreated = DateTime.UtcNow,
                IsDraft = document.IsDraft
            };

            _db.Documents.Add(newDocument);
            await _db.SaveChangesAsync();
            await _db.Entry(newDocument).Reference(d => d.Creator).LoadAsync();

            var newDocumentText = new DocumentText{
                DocumentId = newDocument.DocumentId,
                EditorId = UserId,
                Content = document.Content,
                QuillDelta = document.QuillDelta,
                TimeUpdated = DateTime.UtcNow,
                IsActual = true
            };
            _db.DocumentText.Add(newDocumentText);
            await _db.SaveChangesAsync();

            return Ok(new DocumentDto(newDocument, document.Content));
        }

        [HttpPut("{id:int}")]
        [HttpPut("{slug:regex([[\\w-]])}")]
        public async Task<IActionResult> Update([FromRoute] int? id, [FromRoute] string slug, [FromBody] DocumentDto updatedDocument) {
            if (updatedDocument == null) {
                return BadRequest();
            }

            var document = await GetDocument(id, slug);
            if (document == null) {
                return NotFound();
            }

            document.Title = updatedDocument.Title;
            document.Subtitle = updatedDocument.Subtitle;
            document.IsDraft = updatedDocument.IsDraft;
            document.ParentDocumentId = updatedDocument.ParentDocumentId;

            await _db.SaveChangesAsync();

            var documentText = await _db.DocumentText.SingleOrDefaultAsync(t => t.DocumentId == document.DocumentId && t.IsActual);
            var content = documentText == null ? "" : documentText.Content;

            return Ok(new DocumentDto(document, content));
        }

        [HttpDelete("{id:int}")]
        [HttpDelete("{slug:regex([[\\w-]])}")]
        public async Task<IActionResult> DeleteSingle([FromRoute] int? id, [FromRoute] string slug) {
            var document = await GetDocument(id, slug);
            if (document == null) {
                return NotFound();
            }

            _db.Documents.Remove(document);
            await _db.SaveChangesAsync();
            return Ok();
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteMultiple([FromQuery] int[] ids) {
            _db.Documents.RemoveRange(_db.Documents.Where(u => ids.Contains(u.DocumentId)));
            await _db.SaveChangesAsync();
            return Ok();
        }

        #endregion


        private async Task<Documents> GetDocument(int? id, string slug) {
            IQueryable<Documents> documentQuery = _db.Documents.Include(d => d.Creator);
            Documents document;

            if (id.HasValue) {
                document = await documentQuery.SingleOrDefaultAsync(p => p.DocumentId == id.Value);
            } else {
                document = await documentQuery.SingleOrDefaultAsync(p => p.Slug == slug);
            }

            return document;
        }
    }
}
