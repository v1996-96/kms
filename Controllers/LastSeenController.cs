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
    public class LastSeenController : Controller
    {
        private int UserId { get { return int.Parse(User.Identity.Name); } }

        private readonly KMSDBContext _db;
        public LastSeenController(KMSDBContext context)
        {
            this._db = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetList([FromQuery] int? offset, [FromQuery] int? limit, [FromQuery] string query) {
            var lastSeenQuery = _db.LastSeenDocuments
                .Include(f => f.Document)
                .Where(f => f.UserId == UserId)
                .OrderByDescending(f => f.TimeCreated);

            var count = await lastSeenQuery.CountAsync();
            var following = await lastSeenQuery.Skip(offset.HasValue ? offset.Value : 0).Take(limit.HasValue ? limit.Value : 50).ToListAsync();
            var results = following.Select(b => new LastSeenDto(b));

            return Ok(new { count, results });
        }

        [HttpPost]
        public async Task<IActionResult> MarkSeen([FromBody] LastSeenCreateDto document) {
            if (document == null) {
                return BadRequest();
            }

            var dbDocument = await _db.Documents.SingleOrDefaultAsync(d => d.DocumentId == document.DocumentId);
            if (dbDocument == null) {
                return NotFound();
            }

            var newLastSeen = new LastSeenDocuments { UserId = UserId, DocumentId = document.DocumentId, TimeCreated = DateTime.UtcNow };
            _db.LastSeenDocuments.Add(newLastSeen);
            await _db.SaveChangesAsync();
            await _db.Entry(newLastSeen).Reference(l => l.Document).LoadAsync();

            return Ok(new LastSeenDto(newLastSeen));
        }

        [HttpDelete]
        public async Task<IActionResult> Delete() {
            var lastSeenDocumentsHistory = await _db.LastSeenDocuments.Where(l => l.UserId == UserId).ToListAsync();
            _db.LastSeenDocuments.RemoveRange(lastSeenDocumentsHistory);
            await _db.SaveChangesAsync();
            return Ok();
        }
    }
}
