using System;
using System.Collections.Generic;

namespace kms.Data.Entities
{
    public partial class ProjectRolePermissions
    {
        public int ProjectRoleId { get; set; }
        public string ProjectPermissionSlug { get; set; }

        public ProjectPermissions ProjectPermissionSlugNavigation { get; set; }
        public ProjectRoles ProjectRole { get; set; }
    }
}
