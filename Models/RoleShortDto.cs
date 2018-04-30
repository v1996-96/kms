using kms.Data.Entities;

namespace kms.Models
{
    public class RoleShortDto
    {
        public RoleShortDto(Roles role)
        {
            if (role != null) {
                RoleId = role.RoleId;
                Name = role.Name;
            }
        }
        public int RoleId { get; set; }
        public string Name { get; set; }
    }
}
