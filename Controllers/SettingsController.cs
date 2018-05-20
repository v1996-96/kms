using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using kms.Data.Entities;
using kms.Data;
using kms.Models;
using kms.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using kms.Utils;

namespace kms.Controllers
{
    [KmsController, Authorize]
    public class SettingsController : Controller
    {
        private int UserId { get { return int.Parse(User.Identity.Name); } }

        private readonly KMSDBContext _db;
        public SettingsController(KMSDBContext context)
        {
            this._db = context;
        }

        [HttpGet("permissions")]
        public async Task<IActionResult> GetPermissions() {
            var dbPermissions = await _db.Permissions.ToListAsync();

            if (dbPermissions == null) {
                return NotFound();
            }

            var permissions = dbPermissions.Select(p => new PermissionDto(p));
            return Ok(new { permissions });
        }

        [HttpGet("roles")]
        public async Task<IActionResult> GetRolesList([FromQuery] int? offset, [FromQuery] int? limit) {
            var rolesQuery = _db.Roles.OrderByDescending(r => r.RoleId);
            var count = await rolesQuery.CountAsync();
            var roles = await rolesQuery.Skip(offset.HasValue ? offset.Value : 0).Take(limit.HasValue ? limit.Value : 50).ToListAsync();
            var results = roles.Select(b => new RoleShortDto(b));
            return Ok(new { count, results });
        }

        [HttpGet("roles/{id:int}")]
        public async Task<IActionResult> GetRole([FromRoute] int id) {
            var role = await _db.Roles.Include(r => r.RolePermissions).ThenInclude(rp => rp.PermissionSlugNavigation).SingleOrDefaultAsync(r => r.RoleId == id);

            if (role == null) {
                return NotFound();
            }

            return Ok(new RoleDto(role));
        }

        [HttpPost("roles")]
        public async Task<IActionResult> CreateRole([FromBody] RoleDto role) {
            if (role == null) {
                return BadRequest();
            }

            var newRole = new Roles{ Name = role.Name, System = false };
            _db.Roles.Add(newRole);
            await _db.SaveChangesAsync();

            if (role.Permissions != null) {
                var permissionSlugs = role.Permissions.Select(p => p.PermissionSlug);
                var existingPermissions = await _db.Permissions.Where(p => permissionSlugs.Contains(p.PermissionSlug)).ToListAsync();
                _db.RolePermissions.AddRange(existingPermissions.Select(p => new RolePermissions{ RoleId = newRole.RoleId, PermissionSlug = p.PermissionSlug }));
                await _db.SaveChangesAsync();
            }

            return Ok(new RoleDto(newRole));
        }

        [HttpPut("roles/{id:int}")]
        public async Task<IActionResult> UpdateRole([FromRoute] int id, [FromBody] RoleDto updatedRole) {
            if (updatedRole == null) {
                return BadRequest();
            }

            var role = await _db.Roles.Include(r => r.RolePermissions).ThenInclude(rp => rp.PermissionSlugNavigation).SingleOrDefaultAsync(r => r.RoleId == id);
            if (role == null) {
                return NotFound();
            }

            if (role.System) {
                throw new Exception("System role is immutable");
            }

            role.Name = updatedRole.Name;

            if (role.RolePermissions == null) {
                role.RolePermissions = new List<RolePermissions>();
            }

            if (updatedRole.Permissions != null) {
                var toDelete = role.RolePermissions.Where(uc => !updatedRole.Permissions.Select(c => c.PermissionSlug).Contains(uc.PermissionSlug) && uc.RoleId == id).Select(uc => uc.PermissionSlug);
                var toAdd = updatedRole.Permissions.Where(u => !role.RolePermissions.Select(uc => uc.PermissionSlug).Contains(u.PermissionSlug)).Select(c => c.PermissionSlug);
                _db.RolePermissions.RemoveRange(_db.RolePermissions.Where(uc => toDelete.Contains(uc.PermissionSlug) && uc.RoleId == id));
                var existingPermissionsToAdd = await _db.Permissions.Where(uc => toAdd.Contains(uc.PermissionSlug)).ToListAsync();
                _db.RolePermissions.AddRange(existingPermissionsToAdd.Select(c => new RolePermissions{ RoleId = id, PermissionSlug = c.PermissionSlug }));
            }

            await _db.SaveChangesAsync();
            return Ok(new RoleDto(role));
        }

        [HttpDelete("roles/{id:int}")]
        public async Task<IActionResult> DeleteRole([FromRoute] int id) {
            var role = await _db.Roles.SingleOrDefaultAsync(r => r.RoleId == id);

            if (role == null) {
                return NotFound();
            }
            if (role.System) {
                throw new Exception("System role is immutable");
            }

            _db.Roles.Remove(role);
            await _db.SaveChangesAsync();
            return Ok();
        }

        [HttpDelete("roles")]
        public async Task<IActionResult> DeleteRoles([FromQuery] int[] ids) {
            _db.Roles.RemoveRange(_db.Roles.Where(r => r.System == false && ids.Contains(r.RoleId)));
            await _db.SaveChangesAsync();
            return Ok();
        }
    }
}
