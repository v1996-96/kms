using System;
using System.Collections.Generic;

namespace kms.Data.Entities
{
    public partial class Permissions
    {
        public Permissions()
        {
            RolePermissions = new HashSet<RolePermissions>();
        }

        public string PermissionSlug { get; set; }
        public string Name { get; set; }

        public ICollection<RolePermissions> RolePermissions { get; set; }
    }
}
