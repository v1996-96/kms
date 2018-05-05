using System;
using kms.Data.Entities;

namespace kms.Models
{
    public class AttachmentDto
    {
        public AttachmentDto(Attachments attachment)
        {
            if (attachment != null) {
                AttachmentId = attachment.AttachmentId;
                DocumentId = attachment.DocumentId;
                UserId = attachment.UserId;
                Name = attachment.Name;
                Link = attachment.Link;
                Type = attachment.Type;
                TimeCreated = attachment.TimeCreated;
            }
        }
        public int AttachmentId { get; set; }
        public int DocumentId { get; set; }
        public int? UserId { get; set; }
        public string Name { get; set; }
        public string Link { get; set; }
        public string Type { get; set; }
        public DateTime TimeCreated { get; set; }
    }
}
