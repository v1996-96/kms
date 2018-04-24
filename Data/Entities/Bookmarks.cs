using System;
using System.Collections.Generic;

namespace kms.Data.Entities
{
    public partial class Bookmarks
    {
        public int BookmarkId { get; set; }
        public int UserId { get; set; }
        public int DocumentId { get; set; }
        public DateTime TimeCreated { get; set; }

        public Documents Document { get; set; }
        public Users User { get; set; }
    }
}
