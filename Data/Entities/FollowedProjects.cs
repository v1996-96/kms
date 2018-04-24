using System;
using System.Collections.Generic;

namespace kms.Data.Entities
{
    public partial class FollowedProjects
    {
        public int FollowedProjectsId { get; set; }
        public int UserId { get; set; }
        public int ProjectId { get; set; }
        public DateTime TimeCreated { get; set; }

        public Projects Project { get; set; }
        public Users User { get; set; }
    }
}
