using System;
using System.Collections.Generic;

namespace kms.Data.Entities
{
    public partial class ProjectRolePermissions
    {
        public int ProjectRoleId { get; set; }
        public int ProjectPermissionId { get; set; }

        public ProjectPermissions ProjectPermission { get; set; }
        public ProjectRoles ProjectRole { get; set; }
    }
}
