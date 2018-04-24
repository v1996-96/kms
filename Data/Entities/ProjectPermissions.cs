using System;
using System.Collections.Generic;

namespace kms.Data.Entities
{
    public partial class ProjectPermissions
    {
        public ProjectPermissions()
        {
            ProjectRolePermissions = new HashSet<ProjectRolePermissions>();
        }

        public string ProjectPermissionSlug { get; set; }
        public string Name { get; set; }

        public ICollection<ProjectRolePermissions> ProjectRolePermissions { get; set; }
    }
}
