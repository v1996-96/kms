using System;
using System.Collections.Generic;

namespace kms.Data.Entities
{
    public partial class ProjectRoles
    {
        public ProjectRoles()
        {
            ProjectRolePermissions = new HashSet<ProjectRolePermissions>();
            ProjectTeam = new HashSet<ProjectTeam>();
        }

        public int ProjectRoleId { get; set; }
        public string Name { get; set; }
        public bool System { get; set; }

        public ICollection<ProjectRolePermissions> ProjectRolePermissions { get; set; }
        public ICollection<ProjectTeam> ProjectTeam { get; set; }
    }
}
