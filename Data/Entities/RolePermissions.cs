using System;
using System.Collections.Generic;

namespace kms.Data.Entities
{
    public partial class RolePermissions
    {
        public int RoleId { get; set; }
        public int PermissionId { get; set; }

        public Permissions Permission { get; set; }
        public Roles Role { get; set; }
    }
}
