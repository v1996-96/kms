using kms.Data.Entities;

namespace kms.Models
{
    public class ProjectPermissionDto
    {
        public ProjectPermissionDto(ProjectPermissions permission)
        {
            if (permission != null) {
                ProjectPermissionSlug = permission.ProjectPermissionSlug;
                Name = permission.Name;
            }
        }
        public string ProjectPermissionSlug { get; set; }
        public string Name { get; set; }
    }
}
