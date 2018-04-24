using System;
using System.Collections.Generic;

namespace kms.Data.Entities
{
    public partial class Attachments
    {
        public int AttachmentId { get; set; }
        public int DocumentId { get; set; }
        public int? UserId { get; set; }
        public string Name { get; set; }
        public string Link { get; set; }
        public string Type { get; set; }
        public DateTime TimeCreated { get; set; }

        public Documents Document { get; set; }
        public Users User { get; set; }
    }
}
