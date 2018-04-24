using System;
using System.Collections.Generic;

namespace kms.Data.Entities
{
    public partial class QuickLinks
    {
        public int QuickLinkId { get; set; }
        public int? ProjectId { get; set; }
        public int? DocumentId { get; set; }
        public int? UserId { get; set; }
        public string ExternalLink { get; set; }
        public string Name { get; set; }

        public Documents Document { get; set; }
        public Projects Project { get; set; }
        public Users User { get; set; }
    }
}
