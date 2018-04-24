using System;
using System.Collections.Generic;

namespace kms.Data.Entities
{
    public partial class Activity
    {
        public int ActivityId { get; set; }
        public int ProjectId { get; set; }
        public int? UserId { get; set; }
        public string Content { get; set; }
        public DateTime TimeFired { get; set; }
        public string Meta { get; set; }

        public Projects Project { get; set; }
        public Users User { get; set; }
    }
}
