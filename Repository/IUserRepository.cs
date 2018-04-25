using System.Collections.Generic;
using System.Threading.Tasks;
using kms.Data.Entities;
using kms.Models;

namespace kms.Repository
{
    public interface IUserRepository
    {
        Task<ProfileDto> GetProfile(int id);
        Task<ICollection<Roles>> GetRoles(int id);
        Task<ICollection<Permissions>> GetPermissions(int id);
    }
}
