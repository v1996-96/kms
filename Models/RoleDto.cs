using System.Collections.Generic;
using System.Linq;
using kms.Data.Entities;

namespace kms.Models
{
    public class RoleDto
    {
        public RoleDto(Roles role)
        {
            if (role != null) {
                RoleId = role.RoleId;
                Name = role.Name;
                System = role.System;

                if (role.RolePermissions != null) {
                    Permissions = role.RolePermissions.Where(rp => rp.PermissionSlugNavigation != null).Select(rp => new PermissionDto(rp.PermissionSlugNavigation));
                }
            }
        }
        public int RoleId { get; set; }
        public string Name { get; set; }
        public bool System { get; set; }
        public IEnumerable<PermissionDto> Permissions { get; set; }
    }
}
