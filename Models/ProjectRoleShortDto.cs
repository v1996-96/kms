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
            }
        }
        public int ProjectRoleId { get; set; }
        public string Name { get; set; }
    }
}
