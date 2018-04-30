using kms.Data.Entities;

namespace kms.Models
{
    public class PermissionDto
    {
        public PermissionDto(Permissions permission)
        {
            if (permission != null) {
                PermissionSlug = permission.PermissionSlug;
                Name = permission.Name;
            }
        }
        public string PermissionSlug { get; set; }
        public string Name { get; set; }
    }
}
