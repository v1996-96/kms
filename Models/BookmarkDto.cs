using System;
using kms.Data.Entities;

namespace kms.Models
{
    public class BookmarkDto
    {
        public BookmarkDto(Bookmarks bookmark)
        {
            if (bookmark != null) {
                BookmarkId = bookmark.BookmarkId;
                UserId = bookmark.UserId;
                TimeCreated = bookmark.TimeCreated;
                DocumentId = bookmark.DocumentId;

                if (bookmark.Document != null) {
                    DocumentTitle = bookmark.Document.Title;
                    DocumentSlug = bookmark.Document.Slug;
                }
            }
        }

        public int BookmarkId { get; set; }
        public int UserId { get; set; }
        public DateTime TimeCreated { get; set; }
        public int DocumentId { get; set; }
        public string DocumentTitle { get; set; }
        public string DocumentSlug { get; set; }
    }
}
