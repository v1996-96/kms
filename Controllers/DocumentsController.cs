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
using kms.Services;

namespace kms.Controllers
{
    [KmsController, Authorize]
    public class DocumentsController : Controller
    {
        private int UserId { get { return int.Parse(User.Identity.Name); } }
        private readonly IAssetsService _assets;
        private readonly ISearchRepository _search;
        private readonly KMSDBContext _db;
        private const string ATTACHMENTS_DIRNAME = "attachments";
        public DocumentsController(KMSDBContext context, IAssetsService assets, ISearchRepository search)
        {
            this._assets = assets;
            this._search = search;
            this._db = context;
        }

        [HttpPost("{id:int}/like")]
        [HttpPost("{slug:regex([[\\w-]])}/like")]
        public async Task<IActionResult> LikeDocument([FromRoute] int? id, [FromRoute] string slug) {
            var document = await GetDocument(id, slug);
            if (document == null)
            {
                return NotFound();
            }

            var existingLike = await _db.DocumentLikes.SingleOrDefaultAsync(l => l.UserId == UserId && l.DocumentId == document.DocumentId);
            if (existingLike == null)
            {
                var like = new DocumentLikes
                {
                    UserId = UserId,
                    DocumentId = document.DocumentId,
                    TimeCreated = DateTime.UtcNow
                };
                _db.DocumentLikes.Add(like);
            }
            else
            {
                _db.DocumentLikes.Remove(existingLike);
            }

            await _db.SaveChangesAsync();

            return Ok();
        }

        [HttpPost("{id:int}/save")]
        [HttpPost("{slug:regex([[\\w-]])}/save")]
        public async Task<IActionResult> SaveChanges([FromRoute] int? id, [FromRoute] string slug, [FromBody] DocumentTextSaveDto changes) {
            var document = await GetDocument(id, slug);
            if (document == null) {
                return NotFound();
            }

            var newText = new DocumentText{
                DocumentId = document.DocumentId,
                EditorId = UserId,
                Content = changes.Content,
                QuillDelta = changes.QuillDelta,
                TimeUpdated = DateTime.UtcNow
            };
            _db.DocumentText.Add(newText);
            await _db.SaveChangesAsync();
            return Ok();
        }


        #region CRUD

        [HttpGet]
        public async Task<IActionResult> GetList(
            [FromQuery] int? project,
            [FromQuery] bool? root,
            [FromQuery] int? children,
            [FromQuery] int? offset,
            [FromQuery] int? limit,
            [FromQuery] string query,
            [FromQuery] bool? isDraft)
        {
            IQueryable<Documents> documentsQuery = _db.Documents;

            if (project.HasValue) {
                // If project specified, retrieve documents for project
                documentsQuery = documentsQuery.Where(d => d.ProjectId == project);
            } else {
                // By default retrieve user's documents
                documentsQuery = documentsQuery.Where(d => d.CreatorId == UserId);
            }

            if (isDraft.HasValue) {
                documentsQuery = documentsQuery.Where(d => d.IsDraft == isDraft.Value);
            }

            if (root.HasValue) {
                documentsQuery = documentsQuery.Where(d => d.ParentDocumentId == null);
            } else if (children.HasValue) {
                documentsQuery = documentsQuery.Where(d => d.ParentDocumentId == children.Value);
            }

            if (query.IsValidQuery()) {
                documentsQuery = _search.SearchDocuments(documentsQuery, query);
            } else {
                documentsQuery = documentsQuery.OrderByDescending(d => d.DateCreated);
            }

            var count = await documentsQuery.CountAsync();
            var results = await documentsQuery.Skip(offset.HasValue ? offset.Value : 0).Take(limit.HasValue ? limit.Value : 50)
                .Include(d => d.DocumentLikes).Include(d => d.ParentDocument).Include(d => d.Project).Select(d => new DocumentShortDto(d, d.DocumentLikes.Count())).ToListAsync();
            return Ok(new { count, results });
        }

        [HttpGet("{id:int}")]
        [HttpGet("{slug:regex([[\\w-]])}")]
        public async Task<IActionResult> GetSingle([FromRoute] int? id, [FromRoute] string slug, [FromQuery] bool? quill)
        {
            var document = await GetDocument(id, slug);
            if (document == null)
            {
                return NotFound();
            }

            return Ok(await PrepareDocument(document, quill));
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] DocumentCreateDto document)
        {
            if (document == null)
            {
                return BadRequest();
            }

            int attemptsCount = 0;
            int maxAttemptsCount = 100;
            string slug;
            bool isSlugUnique = false;

            do
            {
                slug = document.Title.GenerateSlug();
                isSlugUnique = (await _db.Documents.SingleOrDefaultAsync(d => d.Slug == slug)) == null;
            } while (!isSlugUnique && attemptsCount < maxAttemptsCount);

            if (!isSlugUnique)
            {
                return BadRequest(new { message = "Cannot generate unique slug" });
            }

            var newDocument = new Documents
            {
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

            var newDocumentText = new DocumentText
            {
                DocumentId = newDocument.DocumentId,
                EditorId = UserId,
                Content = document.Content,
                QuillDelta = document.QuillDelta,
                TimeUpdated = DateTime.UtcNow
            };
            _db.DocumentText.Add(newDocumentText);
            await _db.SaveChangesAsync();

            return Ok(await PrepareDocument(newDocument));
        }

        [HttpPut("{id:int}")]
        [HttpPut("{slug:regex([[\\w-]])}")]
        public async Task<IActionResult> Update([FromRoute] int? id, [FromRoute] string slug, [FromBody] DocumentDto updatedDocument)
        {
            if (updatedDocument == null)
            {
                return BadRequest();
            }

            var document = await GetDocument(id, slug);
            if (document == null)
            {
                return NotFound();
            }

            if (document.Title != updatedDocument.Title)
            {
                int attemptsCount = 0;
                int maxAttemptsCount = 100;
                string newSlug;
                bool isSlugUnique = false;

                do
                {
                    newSlug = updatedDocument.Title.GenerateSlug();
                    isSlugUnique = (await _db.Documents.SingleOrDefaultAsync(d => d.Slug == newSlug)) == null;
                } while (!isSlugUnique && attemptsCount < maxAttemptsCount);

                if (!isSlugUnique)
                {
                    return BadRequest(new { message = "Cannot generate unique slug" });
                }

                document.Title = updatedDocument.Title;
                document.Slug = newSlug;
            }

            document.Subtitle = updatedDocument.Subtitle;
            document.IsDraft = updatedDocument.IsDraft;
            document.ParentDocumentId = updatedDocument.ParentDocumentId;

            await _db.SaveChangesAsync();

            return Ok(await PrepareDocument(document));
        }

        [HttpDelete("{id:int}")]
        [HttpDelete("{slug:regex([[\\w-]])}")]
        public async Task<IActionResult> DeleteSingle([FromRoute] int? id, [FromRoute] string slug)
        {
            var document = await GetDocument(id, slug);
            if (document == null)
            {
                return NotFound();
            }

            _db.Documents.Remove(document);
            await _db.SaveChangesAsync();
            return Ok();
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteMultiple([FromQuery] int[] ids)
        {
            _db.Documents.RemoveRange(_db.Documents.Where(u => ids.Contains(u.DocumentId)));
            await _db.SaveChangesAsync();
            return Ok();
        }

        #endregion


        #region Comments
        [HttpGet("{documentId:int}/comments")]
        [HttpGet("{slug:regex([[\\w-]])}/comments")]
        public async Task<IActionResult> GetComments([FromRoute] int? documentId, [FromRoute] string slug, [FromQuery] int? offset, [FromQuery] int? limit, [FromQuery] string query)
        {
            var document = await GetDocument(documentId, slug);
            if (document == null)
            {
                return NotFound();
            }

            var commentsQuery = _db.Comments.Include(c => c.User).Where(c => c.DocumentId == document.DocumentId).OrderByDescending(c => c.TimeCreated);
            var count = await commentsQuery.CountAsync();
            var results = await commentsQuery.Skip(offset.HasValue ? offset.Value : 0).Take(limit.HasValue ? limit.Value : 50).Include(c => c.CommentLikes).Select(c => new CommentDto(c, c.CommentLikes.Count())).ToListAsync();
            return Ok(new { count, results });
        }

        [HttpPost("{documentId:int}/comments")]
        [HttpPost("{slug:regex([[\\w-]])}/comments")]
        public async Task<IActionResult> CreateComment([FromRoute] int? documentId, [FromRoute] string slug, [FromBody] CommentCreateDto comment)
        {
            var document = await GetDocument(documentId, slug);
            if (document == null)
            {
                return NotFound();
            }

            if (comment == null)
            {
                return BadRequest();
            }

            var newComment = new Comments
            {
                DocumentId = document.DocumentId,
                UserId = UserId,
                Content = comment.Content,
                TimeCreated = DateTime.UtcNow
            };
            _db.Comments.Add(newComment);
            await _db.SaveChangesAsync();
            await _db.Entry(newComment).Reference(c => c.User).LoadAsync();
            return Ok(new CommentDto(newComment));
        }

        [HttpPut("{documentId:int}/comments/{commentId:int}")]
        [HttpPut("{slug:regex([[\\w-]])}/comments/{commentId:int}")]
        public async Task<IActionResult> UpdateComment([FromRoute] int? documentId, [FromRoute] string slug, [FromRoute] int commentId, [FromBody] CommentDto updatedComment)
        {
            var document = await GetDocument(documentId, slug);
            if (document == null)
            {
                return NotFound();
            }

            if (updatedComment == null)
            {
                return BadRequest();
            }

            var comment = await _db.Comments.Include(c => c.User).SingleOrDefaultAsync(c => c.CommentId == commentId);
            if (comment == null)
            {
                return NotFound();
            }

            comment.Content = updatedComment.Content;
            await _db.SaveChangesAsync();

            return Ok(new CommentDto(comment));
        }

        [HttpDelete("{documentId:int}/comments/{commentId:int}")]
        [HttpDelete("{slug:regex([[\\w-]])}/comments/{commentId:int}")]
        public async Task<IActionResult> DeleteComment([FromRoute] int? documentId, [FromRoute] string slug, [FromRoute] int commentId)
        {
            var document = await GetDocument(documentId, slug);
            if (document == null)
            {
                return NotFound();
            }

            var comment = await _db.Comments.SingleOrDefaultAsync(c => c.CommentId == commentId);
            if (comment == null)
            {
                return NotFound();
            }

            _db.Comments.Remove(comment);
            await _db.SaveChangesAsync();
            return Ok();
        }

        [HttpDelete("{documentId:int}/comments")]
        [HttpDelete("{slug:regex([[\\w-]])}/comments")]
        public async Task<IActionResult> DeleteComments([FromRoute] int? documentId, [FromRoute] string slug, [FromQuery] int[] ids)
        {
            var document = await GetDocument(documentId, slug);
            if (document == null)
            {
                return NotFound();
            }

            _db.Comments.RemoveRange(_db.Comments.Where(c => ids.Contains(c.CommentId) && c.DocumentId == document.DocumentId));
            await _db.SaveChangesAsync();
            return Ok();
        }

        [HttpPost("{documentId:int}/comments/{commentId:int}/like")]
        [HttpPost("{slug:regex([[\\w-]])}/comments/{commentId:int}/like")]
        public async Task<IActionResult> LikeComment([FromRoute] int? documentId, [FromRoute] string slug, [FromRoute] int commentId)
        {
            var document = await GetDocument(documentId, slug);
            if (document == null)
            {
                return NotFound();
            }

            var comment = await _db.Comments.SingleOrDefaultAsync(c => c.CommentId == commentId);
            if (comment == null)
            {
                return NotFound();
            }

            var existingLike = await _db.CommentLikes.SingleOrDefaultAsync(c => c.CommentId == commentId && c.UserId == UserId);
            if (existingLike == null)
            {
                var newLike = new CommentLikes
                {
                    CommentId = commentId,
                    UserId = UserId,
                    TimeCreated = DateTime.UtcNow
                };
                _db.CommentLikes.Add(newLike);
            }
            else
            {
                _db.CommentLikes.Remove(existingLike);
            }

            await _db.SaveChangesAsync();
            return Ok();
        }
        #endregion


        #region Attachments

        [HttpGet("{documentId:int}/attachments")]
        [HttpGet("{slug:regex([[\\w-]])}/attachments")]
        public async Task<IActionResult> GetAttachments([FromRoute] int? documentId, [FromRoute] string slug, [FromQuery] int? offset, [FromQuery] int? limit, [FromQuery] string query) {
            var document = await GetDocument(documentId, slug);
            if (document == null)
            {
                return NotFound();
            }

            var protocol = Request.IsHttps ? "https://" : "http://";

            var attachmentsQuery = _db.Attachments.Where(c => c.DocumentId == document.DocumentId).OrderByDescending(c => c.TimeCreated);
            var count = await attachmentsQuery.CountAsync();
            var results = await attachmentsQuery.Skip(offset.HasValue ? offset.Value : 0).Take(limit.HasValue ? limit.Value : 50).Select(c => new AttachmentDto(c, protocol + Request.Host.Value)).ToListAsync();
            return Ok(new { count, results });
        }

        [HttpPost("{documentId:int}/attachments")]
        [HttpPost("{slug:regex([[\\w-]])}/attachments")]
        public async Task<IActionResult> CreateAttachment([FromRoute] int? documentId, [FromRoute] string slug, [FromForm] AttachmentCreateDto attachment) {
            var document = await GetDocument(documentId, slug);
            if (document == null) {
                return NotFound();
            }

            if (attachment == null) {
                return BadRequest();
            }

            if (attachment.File == null) {
                return BadRequest();
            }

            var file = await _assets.SaveFile(attachment.File, ATTACHMENTS_DIRNAME);

            var newAttachment = new Attachments{
                DocumentId = document.DocumentId,
                UserId = UserId,
                Name = attachment.Name,
                Link = file.Path,
                Type = "file",
                TimeCreated = DateTime.UtcNow
            };
            _db.Attachments.Add(newAttachment);
            await _db.SaveChangesAsync();

            return Ok(new AttachmentDto(newAttachment));
        }

        [HttpPut("{documentId:int}/attachments/{attachmentId:int}")]
        [HttpPut("{slug:regex([[\\w-]])}/attachments/{attachmentId:int}")]
        public async Task<IActionResult> UpdateAttachment([FromRoute] int? documentId, [FromRoute] string slug, [FromRoute] int attachmentId, [FromBody] AttachmentDto updatedAttachment) {
            var document = await GetDocument(documentId, slug);
            if (document == null) {
                return NotFound();
            }

            if (updatedAttachment == null) {
                return BadRequest();
            }

            var attachment = await _db.Attachments.SingleOrDefaultAsync(c => c.AttachmentId == attachmentId);
            if (attachment == null) {
                return NotFound();
            }

            attachment.Name = updatedAttachment.Name;
            await _db.SaveChangesAsync();
            return Ok(new AttachmentDto(attachment));
        }

        [HttpDelete("{documentId:int}/attachments/{attachmentId:int}")]
        [HttpDelete("{slug:regex([[\\w-]])}/attachments/{attachmentId:int}")]
        public async Task<IActionResult> DeleteAttachment([FromRoute] int? documentId, [FromRoute] string slug, [FromRoute] int attachmentId) {
            var document = await GetDocument(documentId, slug);
            if (document == null) {
                return NotFound();
            }

            var attachment = await _db.Attachments.SingleOrDefaultAsync(c => c.AttachmentId == attachmentId);
            if (attachment == null) {
                return NotFound();
            }

            _db.Attachments.Remove(attachment);
            await _db.SaveChangesAsync();
            return Ok();
        }

        [HttpDelete("{documentId:int}/attachments")]
        [HttpDelete("{slug:regex([[\\w-]])}/attachments")]
        public async Task<IActionResult> DeleteAttachments([FromRoute] int? documentId, [FromRoute] string slug, [FromQuery] int[] ids) {
            var document = await GetDocument(documentId, slug);
            if (document == null) {
                return NotFound();
            }

            _db.Attachments.RemoveRange(_db.Attachments.Where(c => ids.Contains(c.AttachmentId) && c.DocumentId == document.DocumentId));
            await _db.SaveChangesAsync();
            return Ok();
        }

        #endregion


        private async Task<Documents> GetDocument(int? id, string slug) {
            IQueryable<Documents> documentQuery = _db.Documents.Include(d => d.Creator).Include(d => d.Project).Include(d => d.ParentDocument);
            Documents document;

            if (id.HasValue)
            {
                document = await documentQuery.SingleOrDefaultAsync(p => p.DocumentId == id.Value);
            }
            else
            {
                document = await documentQuery.SingleOrDefaultAsync(p => p.Slug == slug);
            }

            return document;
        }

        private async Task<DocumentDto> PrepareDocument(Documents document, bool? quill = false) {
            var documentText = await _db.DocumentText.SingleOrDefaultAsync(t => t.DocumentId == document.DocumentId && t.IsActual);
            var content = documentText == null ? "" : documentText.Content;
            var quillDelta = documentText != null && quill.HasValue && quill.Value ? documentText.QuillDelta : "";
            var likesCount = await _db.DocumentLikes.Where(l => l.DocumentId == document.DocumentId).CountAsync();
            return new DocumentDto(document, content, likesCount, quillDelta);
        }
    }
}
