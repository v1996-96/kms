using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using kms.Data;
using kms.Data.Entities;
using kms.Models;
using Microsoft.EntityFrameworkCore;

namespace kms.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly KMSDBContext _db;
        public UserRepository(KMSDBContext context)
        {
            this._db = context;
        }

        public async Task<ICollection<Permissions>> GetPermissions(int id)
            => await _db.Permissions
                .Include(p => p.RolePermissions)
                .ThenInclude(rp => rp.Role)
                .ThenInclude(r => r.UserRoles)
                .Where(p => p.RolePermissions.All(rp => rp.Role.UserRoles.All(ur => ur.UserId == id)))
                .ToListAsync();

        public async Task<ICollection<Roles>> GetRoles(int id)
            => await _db.Roles
                .Include(r => r.UserRoles)
                .Where(r => r.UserRoles.All(ur => ur.UserId == id))
                .ToListAsync();

        public async Task<ProfileDto> GetProfile(int id)
        {
            var user = await _db.Users
                .Include(u => u.UserRoles)
                .ThenInclude(ur => ur.Role)
                .ThenInclude(r => r.RolePermissions)
                .ThenInclude(rp => rp.PermissionSlugNavigation)
                .SingleOrDefaultAsync(u => u.UserId == id);

            return new ProfileDto(user, await GetRoles(id), await GetPermissions(id));
        }
    }
}
