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
    public class TemplatesController : Controller
    {
        private int UserId { get { return int.Parse(User.Identity.Name); } }
        private readonly ISearchRepository _search;
        private readonly KMSDBContext _db;
        private const string ATTACHMENTS_DIRNAME = "attachments";
        public TemplatesController(KMSDBContext context, ISearchRepository search)
        {
            this._search = search;
            this._db = context;
        }


        [HttpGet("types")]
        public async Task<IActionResult> GetTemplateTypes() {
            var types = await _db.TemplateTypes.OrderByDescending(t => t.System).ToListAsync();
            return Ok(new { types = types.Select(t => new TemplateTypeDto(t)) });
        }

        [HttpGet]
        public async Task<IActionResult> GetList([FromQuery] int? project, [FromQuery] string type, [FromQuery] int? offset, [FromQuery] int? limit, [FromQuery] string query)
        {
            IQueryable<Templates> templatesQuery = _db.Templates;

            if (type != null && type != "") {
                templatesQuery = templatesQuery.Where(d => d.TemplateTypeSlug == type);
            }

            if (project.HasValue) {
                templatesQuery = templatesQuery.Where(d => d.ProjectId == project);
            } else {
                templatesQuery = templatesQuery.Where(d => d.ProjectId == null);
            }

            if (query.IsValidQuery()) {
                templatesQuery = _search.SearchTemplates(templatesQuery, query);
            } else {
                templatesQuery = templatesQuery.OrderByDescending(d => d.DateCreated);
            }

            var count = await templatesQuery.CountAsync();
            var results = await templatesQuery.Skip(offset.HasValue ? offset.Value : 0).Take(limit.HasValue ? limit.Value : 50).Select(d => new TemplateShortDto(d)).ToListAsync();
            return Ok(new { count, results });
        }

        [HttpGet("type/{slug:regex([[\\w-]])}")]
        public async Task<IActionResult> GetSingleByType([FromRoute] string slug, [FromQuery] int? project) {
            IQueryable<Templates> templateQuery = _db.Templates.Include(t => t.Creator);

            if (project.HasValue) {
                templateQuery = templateQuery.Where(t => t.ProjectId == project.Value);
            }

            var template = await templateQuery.SingleOrDefaultAsync(t => t.TemplateTypeSlug == slug);
            if (template == null) {
                return NotFound();
            }

            var templateText = await _db.TemplateText.SingleOrDefaultAsync(t => t.TemplateId == template.TemplateId && t.IsActual);
            var content = templateText == null ? "" : templateText.Content;
            return Ok(new TemplateDto(template, content));
        }

        [HttpGet("{id:int}")]
        [HttpGet("{slug:regex([[\\w-]])}")]
        public async Task<IActionResult> GetSingle([FromRoute] int? id, [FromRoute] string slug, [FromQuery] bool? quill)
        {
            var template = await GetTemplate(id, slug);
            if (template == null)
            {
                return NotFound();
            }

            var templateText = await _db.TemplateText.SingleOrDefaultAsync(t => t.TemplateId == template.TemplateId && t.IsActual);
            var content = templateText == null ? "" : templateText.Content;
            var quillDelta = templateText != null && quill.HasValue && quill.Value ? templateText.QuillDelta : "";
            return Ok(new TemplateDto(template, content, quillDelta));
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] TemplateCreateDto template)
        {
            if (template == null)
            {
                return BadRequest();
            }

            int attemptsCount = 0;
            int maxAttemptsCount = 100;
            string slug;
            bool isSlugUnique = false;

            do
            {
                slug = template.Title.GenerateSlug();
                isSlugUnique = (await _db.Templates.SingleOrDefaultAsync(d => d.Slug == slug)) == null;
            } while (!isSlugUnique && attemptsCount < maxAttemptsCount);

            if (!isSlugUnique)
            {
                return BadRequest(new { message = "Cannot generate unique slug" });
            }

            var newTemplate = new Templates
            {
                ProjectId = template.ProjectId,
                TemplateTypeSlug = template.TemplateType,
                CreatorId = UserId,
                Slug = slug,
                Title = template.Title,
                Description = template.Description,
                DateCreated = DateTime.UtcNow
            };

            _db.Templates.Add(newTemplate);
            await _db.SaveChangesAsync();
            await _db.Entry(newTemplate).Reference(d => d.Creator).LoadAsync();

            var newTemplateText = new TemplateText
            {
                TemplateId = newTemplate.TemplateId,
                EditorId = UserId,
                Content = template.Content,
                QuillDelta = template.QuillDelta,
                TimeUpdated = DateTime.UtcNow
            };
            _db.TemplateText.Add(newTemplateText);
            await _db.SaveChangesAsync();

            return Ok(new TemplateDto(newTemplate, template.Content));
        }

        [HttpPost("{id:int}/save")]
        [HttpPost("{slug:regex([[\\w-]])}/save")]
        public async Task<IActionResult> SaveChanges([FromRoute] int? id, [FromRoute] string slug, [FromBody] TemplateTextSaveDto changes) {
            var template = await GetTemplate(id, slug);
            if (template == null) {
                return NotFound();
            }

            var newText = new TemplateText{
                TemplateId = template.TemplateId,
                EditorId = UserId,
                Content = changes.Content,
                QuillDelta = changes.QuillDelta,
                TimeUpdated = DateTime.UtcNow
            };
            _db.TemplateText.Add(newText);
            await _db.SaveChangesAsync();
            return Ok();
        }

        [HttpPut("{id:int}")]
        [HttpPut("{slug:regex([[\\w-]])}")]
        public async Task<IActionResult> Update([FromRoute] int? id, [FromRoute] string slug, [FromBody] TemplateDto updatedTemplate)
        {
            if (updatedTemplate == null)
            {
                return BadRequest();
            }

            var document = await GetTemplate(id, slug);
            if (document == null)
            {
                return NotFound();
            }

            if (document.Title != updatedTemplate.Title)
            {
                int attemptsCount = 0;
                int maxAttemptsCount = 100;
                string newSlug;
                bool isSlugUnique = false;

                do
                {
                    newSlug = updatedTemplate.Title.GenerateSlug();
                    isSlugUnique = (await _db.Templates.SingleOrDefaultAsync(d => d.Slug == newSlug)) == null;
                } while (!isSlugUnique && attemptsCount < maxAttemptsCount);

                if (!isSlugUnique)
                {
                    return BadRequest(new { message = "Cannot generate unique slug" });
                }

                document.Title = updatedTemplate.Title;
                document.Slug = newSlug;
            }

            document.Description = updatedTemplate.Description;
            document.TemplateTypeSlug = updatedTemplate.TemplateType;

            await _db.SaveChangesAsync();

            var documentText = await _db.TemplateText.SingleOrDefaultAsync(t => t.TemplateId == document.TemplateId && t.IsActual);
            var content = documentText == null ? "" : documentText.Content;

            return Ok(new TemplateDto(document, content));
        }

        [HttpDelete("{id:int}")]
        [HttpDelete("{slug:regex([[\\w-]])}")]
        public async Task<IActionResult> DeleteSingle([FromRoute] int? id, [FromRoute] string slug)
        {
            var template = await GetTemplate(id, slug);
            if (template == null)
            {
                return NotFound();
            }

            _db.Templates.Remove(template);
            await _db.SaveChangesAsync();
            return Ok();
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteMultiple([FromQuery] int[] ids)
        {
            _db.Templates.RemoveRange(_db.Templates.Where(u => ids.Contains(u.TemplateId)));
            await _db.SaveChangesAsync();
            return Ok();
        }


        private async Task<Templates> GetTemplate(int? id, string slug) {
            IQueryable<Templates> templateQuery = _db.Templates.Include(t => t.Creator);
            Templates template;

            if (id.HasValue) {
                template = await templateQuery.SingleOrDefaultAsync(t => t.TemplateId == id.Value);
            } else {
                template = await templateQuery.SingleOrDefaultAsync(t => t.Slug == slug);
            }

            return template;
        }
    }
}
