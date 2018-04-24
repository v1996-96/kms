using System;
using System.Collections.Generic;

namespace kms.Data.Entities
{
    public partial class RolePermissions
    {
        public int RoleId { get; set; }
        public string PermissionSlug { get; set; }

        public Permissions PermissionSlugNavigation { get; set; }
        public Roles Role { get; set; }
    }
}
