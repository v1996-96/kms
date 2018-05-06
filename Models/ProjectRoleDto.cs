using System.Collections.Generic;
using System.Linq;
using kms.Data.Entities;

namespace kms.Models
{
    public class ProjectRoleDto
    {
        public ProjectRoleDto(ProjectRoles role)
        {
            if (role != null) {
                ProjectRoleId = role.ProjectRoleId;
                Name = role.Name;
                System = role.System;

                if (role.ProjectRolePermissions != null) {
                    Permissions = role.ProjectRolePermissions.Where(rp => rp.ProjectPermissionSlugNavigation != null).Select(rp => new ProjectPermissionDto(rp.ProjectPermissionSlugNavigation));
                }
            }
        }
        public int ProjectRoleId { get; set; }
        public string Name { get; set; }
        public bool System { get; set; }
        public IEnumerable<ProjectPermissionDto> Permissions { get; set; }
    }
}
