using System;
using System.Collections.Generic;

namespace kms.Data.Entities
{
    public partial class CommentLikes
    {
        public int CommentLikeId { get; set; }
        public int? UserId { get; set; }
        public int CommentId { get; set; }
        public DateTime TimeCreated { get; set; }

        public Comments Comment { get; set; }
        public Users User { get; set; }
    }
}
