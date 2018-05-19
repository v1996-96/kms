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
    public class BookmarksController : Controller
    {
        private int UserId { get { return int.Parse(User.Identity.Name); } }
        private readonly ISearchRepository _search;
        private readonly KMSDBContext _db;
        public BookmarksController(KMSDBContext context, ISearchRepository search)
        {
            this._search = search;
            this._db = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetList([FromQuery] int? offset, [FromQuery] int? limit, [FromQuery] string query)
        {
            IQueryable<Bookmarks> bookmarksQuery = _db.Bookmarks.Include(b => b.Document).Where(b => b.UserId == UserId);

            if (query.IsValidQuery()) {
                bookmarksQuery = _search.SearchBookmarks(bookmarksQuery, query);
            } else {
                bookmarksQuery = bookmarksQuery.OrderByDescending(b => b.TimeCreated);
            }

            var count = await bookmarksQuery.CountAsync();
            var bookmarks = await bookmarksQuery.Skip(offset.HasValue ? offset.Value : 0).Take(limit.HasValue ? limit.Value : 50).ToListAsync();
            var results = bookmarks.Select(b => new BookmarkDto(b));

            return Ok(new { count, results });
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetByDocument([FromRoute] int id) {
            var document = await _db.Bookmarks.Include(f => f.Document).SingleOrDefaultAsync(f => f.DocumentId == id && f.UserId == UserId);
            if (document == null) {
                return NotFound();
            }

            return Ok(new BookmarkDto(document));
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] BookmarkCreateDto bookmark)
        {
            if (bookmark == null)
            {
                return BadRequest();
            }

            var document = await _db.Documents.SingleOrDefaultAsync(d => d.DocumentId == bookmark.DocumentId);
            if (document == null)
            {
                return NotFound();
            }

            var existingBookmark = await _db.Bookmarks.Include(b => b.Document).SingleOrDefaultAsync(b => b.DocumentId == bookmark.DocumentId && b.UserId == UserId);
            if (existingBookmark != null)
            {
                return Ok(new BookmarkDto(existingBookmark));
            }

            var newBookmark = new Bookmarks { UserId = UserId, DocumentId = bookmark.DocumentId, TimeCreated = DateTime.UtcNow };
            _db.Bookmarks.Add(newBookmark);
            await _db.SaveChangesAsync();
            await _db.Entry(newBookmark).Reference(b => b.Document).LoadAsync();

            return Ok(new BookmarkDto(newBookmark));
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            var bookmark = await _db.Bookmarks.SingleOrDefaultAsync(b => b.BookmarkId == id);

            if (bookmark == null)
            {
                return NotFound();
            }

            _db.Bookmarks.Remove(bookmark);
            await _db.SaveChangesAsync();

            return Ok();
        }
    }
}
