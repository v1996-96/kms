using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using kms.Data;
using kms.Data.Entities;
using kms.Models;
using Microsoft.EntityFrameworkCore;

namespace kms.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly KMSDBContext _db;
        private readonly DbConnection _dbconnection;
        public UserRepository(KMSDBContext context, IKMSDBConnection connection)
        {
            this._dbconnection = connection.Connection;
            this._db = context;
        }

        public async Task<IEnumerable<Permissions>> GetPermissions(int id)
            => await _dbconnection.QueryAsync<Permissions>(
                @"select p.* from permissions as p
                left join role_permissions as rp on rp.permission_slug = p.permission_slug
                left join user_roles as ur on ur.role_id = rp.role_id
                where ur.user_id = @id
                group by p.permission_slug",
                new { id });

        public async Task<IEnumerable<Roles>> GetRoles(int id)
            => await _dbconnection.QueryAsync<Roles>(
                @"select r.* from roles as r
                left join user_roles as ur on ur.role_id = r.role_id
                where ur.user_id = @id
                group by r.role_id", new { id });

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
