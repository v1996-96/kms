using kms.Data.Entities;

namespace kms.Models
{
    public class ProjectRoleShortDto
    {
        public ProjectRoleShortDto(ProjectRoles role)
        {
            if (role != null) {
                ProjectRoleId = role.ProjectRoleId;
                Name = role.Name;
                System = role.System;
            }
        }
        public int ProjectRoleId { get; set; }
        public string Name { get; set; }
        public bool System { get; set; }
    }
}
