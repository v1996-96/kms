using System;
using kms.Data.Entities;

namespace kms.Models
{
    public class CommentDto
    {
        public CommentDto(Comments comment, int likesCount = 0)
        {
            if (comment != null ) {
                CommentId = comment.CommentId;
                Content = comment.Content;
                TimeCreated = comment.TimeCreated;

                if (comment.User != null) {
                    User = new UserShortDto(comment.User);
                }
            }

            LikesCount = likesCount;
        }
        public int CommentId { get; set; }
        public string Content { get; set; }
        public DateTime TimeCreated { get; set; }
        public UserShortDto User { get; set; }
        public int LikesCount { get; set; }
    }
}
