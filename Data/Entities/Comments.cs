using System;
using System.Collections.Generic;

namespace kms.Data.Entities
{
    public partial class Comments
    {
        public Comments()
        {
            CommentLikes = new HashSet<CommentLikes>();
        }

        public int CommentId { get; set; }
        public int DocumentId { get; set; }
        public int? UserId { get; set; }
        public string Content { get; set; }
        public DateTime TimeCreated { get; set; }

        public Documents Document { get; set; }
        public Users User { get; set; }
        public ICollection<CommentLikes> CommentLikes { get; set; }
    }
}
