using System;
using System.Collections.Generic;

namespace kms.Data.Entities
{
    public partial class Roles
    {
        public Roles()
        {
            RolePermissions = new HashSet<RolePermissions>();
            UserRoles = new HashSet<UserRoles>();
        }

        public int RoleId { get; set; }
        public string Name { get; set; }
        public bool System { get; set; }

        public ICollection<RolePermissions> RolePermissions { get; set; }
        public ICollection<UserRoles> UserRoles { get; set; }
    }
}
