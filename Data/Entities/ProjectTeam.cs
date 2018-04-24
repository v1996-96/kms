using System;
using System.Collections.Generic;

namespace kms.Data.Entities
{
    public partial class ProjectTeam
    {
        public int UserId { get; set; }
        public int ProjectId { get; set; }
        public int ProjectRoleId { get; set; }
        public DateTime DateJoined { get; set; }
        public string Position { get; set; }

        public Projects Project { get; set; }
        public ProjectRoles ProjectRole { get; set; }
        public Users User { get; set; }
    }
}
