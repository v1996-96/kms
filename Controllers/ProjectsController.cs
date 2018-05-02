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
    public class ProjectsController : Controller
    {
        private int UserId { get { return int.Parse(User.Identity.Name); } }

        private readonly KMSDBContext _db;
        public ProjectsController(KMSDBContext context)
        {
            this._db = context;
        }

        #region CRUD

        [HttpGet]
        public async Task<IActionResult> GetList([FromQuery] int? user, [FromQuery] int? offset, [FromQuery] int? limit, [FromQuery] string query) {
            IQueryable<Projects> projectsQuery = _db.Projects;

            if (user.HasValue) {
                projectsQuery = projectsQuery.Include(p => p.ProjectTeam).Where(p => p.ProjectTeam.Any(pt => pt.UserId == user.Value));
            }

            projectsQuery = projectsQuery.OrderByDescending(p => p.ProjectId);

            var count = await projectsQuery.CountAsync();
            var projects = await projectsQuery.Skip(offset.HasValue ? offset.Value : 0).Take(limit.HasValue ? limit.Value : 50).ToListAsync();
            var results = projects.Select(p => new ProjectShortDto(p));
            return Ok(new { count, results });
        }

        [HttpGet("{id:int}")]
        [HttpGet("{slug:regex([[\\w-]])}")]
        public async Task<IActionResult> GetSingle([FromRoute] int? id, [FromRoute] string slug) {
            var project = await GetProject(id, slug);
            if (project == null) {
                return NotFound();
            }

            return Ok(new ProjectDto(project));
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] ProjectCreateDto project) {
            if (project == null) {
                return BadRequest();
            }

            int attemptsCount = 0;
            int maxAttemptsCount = 10;
            string slug;
            bool isSlugUnique = false;

            do {
                slug = project.Name.GenerateSlug();
                isSlugUnique = (await _db.Projects.SingleOrDefaultAsync(p => p.Slug == slug)) == null;
            } while (!isSlugUnique && attemptsCount < maxAttemptsCount);

            if (!isSlugUnique) {
                return BadRequest(new { message = "Cannot generate unique slug" });
            }

            var newProject = new Projects{
                Slug = slug,
                Name = project.Name,
                Description = project.Description,
                Goal = project.Goal,
                DateStart = project.DateStart,
                DateEnd = project.DateEnd,
                Avatar = project.Avatar,
                IsOpen = project.IsOpen,
                IsActive = project.IsActive
            };

            _db.Projects.Add(newProject);
            await _db.SaveChangesAsync();
            await _db.Entry(newProject).Collection(b => b.QuickLinksHousingProject).LoadAsync();

            return Ok(new ProjectDto(newProject));
        }

        [HttpPut("{id:int}")]
        [HttpPut("{slug:regex([[\\w-]])}")]
        public async Task<IActionResult> Update([FromRoute] int? id, [FromRoute] string slug, [FromBody] ProjectDto updatedProject) {
            if (updatedProject == null) {
                return BadRequest();
            }

            IQueryable<Projects> projectQuery = _db.Projects.Include(p => p.QuickLinksHousingProject);
            Projects project;

            if (id.HasValue) {
                project = await projectQuery.SingleOrDefaultAsync(p => p.ProjectId == id.Value);
            } else {
                project = await projectQuery.SingleOrDefaultAsync(p => p.Slug == slug);
            }

            if (project == null) {
                return NotFound();
            }

            project.Name = updatedProject.Name;
            project.Description = updatedProject.Description;
            project.Goal = updatedProject.Goal;
            project.DateStart = updatedProject.DateStart;
            project.DateEnd = updatedProject.DateEnd;
            project.Avatar = updatedProject.Avatar;
            project.IsOpen = updatedProject.IsOpen;
            project.IsActive = updatedProject.IsActive;

            await _db.SaveChangesAsync();

            return Ok(new ProjectDto(project));
        }

        [HttpDelete("{id:int}")]
        [HttpDelete("{slug:regex([[\\w-]])}")]
        public async Task<IActionResult> DeleteSingle([FromRoute] int? id, [FromRoute] string slug) {
            var project = await GetProject(id, slug);
            if (project == null) {
                return NotFound();
            }

            _db.Projects.Remove(project);
            await _db.SaveChangesAsync();

            return Ok();
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteMultiple([FromQuery] int[] ids) {
            _db.Projects.RemoveRange(_db.Projects.Where(u => ids.Contains(u.ProjectId)));
            await _db.SaveChangesAsync();
            return Ok();
        }

        #endregion


        #region Access

        [HttpGet("permissions")]
        public async Task<IActionResult> GetPermissions() {
            var dbPermissions = await _db.ProjectPermissions.ToListAsync();

            if (dbPermissions == null) {
                return NotFound();
            }

            return Ok(new { permissions = dbPermissions.Select(p => new ProjectPermissionDto(p)) });
        }

        [HttpGet("roles")]
        public async Task<IActionResult> GetRolesList([FromQuery] int? offset, [FromQuery] int? limit) {
            var rolesQuery = _db.ProjectRoles.OrderByDescending(r => r.ProjectRoleId);
            var count = await rolesQuery.CountAsync();
            var roles = await rolesQuery.Skip(offset.HasValue ? offset.Value : 0).Take(limit.HasValue ? limit.Value : 50).ToListAsync();
            var results = roles.Select(r => new ProjectRoleShortDto(r));
            return Ok(new { count, results });
        }

        [HttpGet("roles/{id:int}")]
        public async Task<IActionResult> GetRole([FromRoute] int id) {
            var role = await _db.ProjectRoles.Include(r => r.ProjectRolePermissions).ThenInclude(rp => rp.ProjectPermissionSlugNavigation).SingleOrDefaultAsync(r => r.ProjectRoleId == id);

            if (role == null) {
                return NotFound();
            }

            return Ok(new ProjectRoleDto(role));
        }

        [HttpPost("roles")]
        public async Task<IActionResult> CreateRole([FromBody] ProjectRoleDto role) {
            if (role == null) {
                return BadRequest();
            }

            var newRole = new ProjectRoles{ Name = role.Name, System = false };
            _db.ProjectRoles.Add(newRole);
            await _db.SaveChangesAsync();

            if (role.Permissions != null) {
                var permissionSlugs = role.Permissions.Select(p => p.ProjectPermissionSlug);
                var existingPermissions = await _db.ProjectPermissions.Where(p => permissionSlugs.Contains(p.ProjectPermissionSlug)).ToListAsync();
                _db.ProjectRolePermissions.AddRange(existingPermissions.Select(p => new ProjectRolePermissions{ ProjectRoleId = newRole.ProjectRoleId, ProjectPermissionSlug = p.ProjectPermissionSlug }));
                await _db.SaveChangesAsync();
            }

            return Ok(new ProjectRoleDto(newRole));
        }

        [HttpPut("roles/{id:int}")]
        public async Task<IActionResult> UpdateRole([FromRoute] int id, [FromBody] ProjectRoleDto updatedRole) {
            if (updatedRole == null) {
                return BadRequest();
            }

            var role = await _db.ProjectRoles.Include(r => r.ProjectRolePermissions).ThenInclude(rp => rp.ProjectPermissionSlugNavigation).SingleOrDefaultAsync(r => r.ProjectRoleId == id);
            if (role == null) {
                return NotFound();
            }

            if (role.System) {
                throw new Exception("System role is immutable");
            }

            role.Name = updatedRole.Name;

            if (role.ProjectRolePermissions == null) {
                role.ProjectRolePermissions = new List<ProjectRolePermissions>();
            }

            if (updatedRole.Permissions != null) {
                var toDelete = role.ProjectRolePermissions.Where(uc => !updatedRole.Permissions.Select(c => c.ProjectPermissionSlug).Contains(uc.ProjectPermissionSlug)).Select(uc => uc.ProjectPermissionSlug);
                var toAdd = updatedRole.Permissions.Where(u => !role.ProjectRolePermissions.Select(uc => uc.ProjectPermissionSlug).Contains(u.ProjectPermissionSlug)).Select(c => c.ProjectPermissionSlug);
                _db.ProjectRolePermissions.RemoveRange(_db.ProjectRolePermissions.Where(uc => toDelete.Contains(uc.ProjectPermissionSlug)));
                var existingPermissionsToAdd = await _db.ProjectPermissions.Where(uc => toAdd.Contains(uc.ProjectPermissionSlug)).ToListAsync();
                _db.ProjectRolePermissions.AddRange(existingPermissionsToAdd.Select(c => new ProjectRolePermissions{ ProjectRoleId = id, ProjectPermissionSlug = c.ProjectPermissionSlug }));
            }

            await _db.SaveChangesAsync();
            return Ok(new ProjectRoleDto(role));
        }

        [HttpDelete("roles/{id:int}")]
        public async Task<IActionResult> DeleteRole([FromRoute] int id) {
            var role = await _db.ProjectRoles.SingleOrDefaultAsync(r => r.ProjectRoleId == id);

            if (role == null) {
                return NotFound();
            }

            if (role.System) {
                throw new Exception("System role is immutable");
            }

            _db.ProjectRoles.Remove(role);
            await _db.SaveChangesAsync();
            return Ok();
        }

        [HttpDelete("roles")]
        public async Task<IActionResult> DeleteRoles([FromQuery] int[] ids) {
            _db.ProjectRoles.RemoveRange(_db.ProjectRoles.Where(r => r.System == false && ids.Contains(r.ProjectRoleId)));
            await _db.SaveChangesAsync();
            return Ok();
        }

        #endregion


        #region Team
        [HttpGet("{projectId:int}/team")]
        [HttpGet("{slug:regex([[\\w-]])}/team")]
        public async Task<IActionResult> GetTeam([FromRoute] int? projectId, [FromRoute] string slug, [FromQuery] int? offset, [FromQuery] int? limit, [FromQuery] string query) {
            IQueryable<ProjectTeam> teamQuery = _db.ProjectTeam.Include(t => t.ProjectRole);

            if (projectId.HasValue) {
                teamQuery = teamQuery.Where(t => t.ProjectId == projectId.Value);
            } else {
                teamQuery = teamQuery.Include(t => t.Project).Where(t => t.Project.Slug == slug);
            }

            teamQuery = teamQuery.OrderByDescending(t => t.DateJoined);

            var count = await teamQuery.CountAsync();
            var team = await teamQuery.Skip(offset.HasValue ? offset.Value : 0).Take(limit.HasValue ? limit.Value : 50).ToListAsync();
            var results = team.Select(t => new ProjectTeamDto(t));
            return Ok(new { count, results });
        }

        [HttpPost("{projectId:int}/team")]
        [HttpPost("{slug:regex([[\\w-]])}/team")]
        public async Task<IActionResult> AddTeamMember([FromRoute] int? projectId, [FromRoute] string slug, [FromBody] ProjectTeamCreateDto member) {
            if (member == null) {
                return BadRequest();
            }

            if (member.ProjectRole == null) {
                return BadRequest(new { message = "You must specify user role" });
            }

            var role = await _db.ProjectRoles.SingleOrDefaultAsync(r => r.ProjectRoleId == member.ProjectRole.ProjectRoleId);
            if (role == null) {
                return BadRequest(new { message = "Role provided was not found" });
            }

            var project = await GetProject(projectId, slug);
            if (project == null) {
                return NotFound();
            }

            var newMember = new ProjectTeam{
                UserId = member.UserId,
                ProjectId = project.ProjectId,
                ProjectRoleId = member.ProjectRole.ProjectRoleId,
                DateJoined = DateTime.UtcNow,
                Position = member.Position
            };
            _db.ProjectTeam.Add(newMember);
            await _db.SaveChangesAsync();
            await _db.Entry(newMember).Reference(t => t.ProjectRole).LoadAsync();

            return Ok(new ProjectTeamDto(newMember));
        }

        [HttpPut("{projectId:int}/team/{userId:int}")]
        [HttpPut("{slug:regex([[\\w-]])}/team/{userId:int}")]
        public async Task<IActionResult> UpdateTeamMember([FromRoute] int? projectId, [FromRoute] string slug, [FromRoute] int userId, [FromBody] ProjectTeamDto updatedMember) {
            if (updatedMember == null) {
                return BadRequest();
            }

            if (updatedMember.ProjectRole == null) {
                return BadRequest(new { message = "You must specify user role" });
            }

            var role = await _db.ProjectRoles.SingleOrDefaultAsync(r => r.ProjectRoleId == updatedMember.ProjectRole.ProjectRoleId);
            if (role == null) {
                return BadRequest(new { message = "Role provided was not found" });
            }

            var project = await GetProject(projectId, slug);
            if (project == null) {
                return NotFound();
            }

            var member = await _db.ProjectTeam.Include(t => t.ProjectRole).SingleOrDefaultAsync(t => t.UserId == updatedMember.UserId && t.ProjectId == project.ProjectId);
            if (member == null) {
                return NotFound();
            }

            member.Position = updatedMember.Position;
            member.ProjectRoleId = updatedMember.ProjectRole.ProjectRoleId;
            await _db.SaveChangesAsync();

            return Ok(new ProjectTeamDto(member));
        }

        [HttpDelete("{projectId:int}/team/{userId:int}")]
        [HttpDelete("{slug:regex([[\\w-]])}/team/{userId:int}")]
        public async Task<IActionResult> DeleteTeamMember([FromRoute] int? projectId, [FromRoute] string slug, [FromRoute] int userId) {
            var project = await GetProject(projectId, slug);
            if (project == null) {
                return NotFound();
            }

            var member = await _db.ProjectTeam.SingleOrDefaultAsync(t => t.UserId == userId && t.ProjectId == project.ProjectId);
            if (member == null) {
                return NotFound();
            }

            _db.ProjectTeam.Remove(member);
            await _db.SaveChangesAsync();

            return Ok();
        }

        [HttpDelete("{projectId:int}/team")]
        [HttpDelete("{slug:regex([[\\w-]])}/team")]
        public async Task<IActionResult> DeleteTeamMembers([FromRoute] int? projectId, [FromRoute] string slug, [FromQuery] int[] ids) {
            var project = await GetProject(projectId, slug);
            if (project == null) {
                return NotFound();
            }

            _db.ProjectTeam.RemoveRange(_db.ProjectTeam.Where(t => ids.Contains(t.UserId) && t.ProjectId == project.ProjectId));
            await _db.SaveChangesAsync();
            return Ok();
        }
        #endregion


        #region Quick links
        [HttpGet("{projectId:int}/quick-links")]
        [HttpGet("{slug:regex([[\\w-]])}/quick-links")]
        public async Task<IActionResult> GetQuickLinks([FromRoute] int? projectId, [FromRoute] string slug, [FromQuery] int? offset, [FromQuery] int? limit, [FromQuery] string query) {
            // IQueryable<QuickLinks> quickLinksQuery = _db.QuickLinks;

            // IQueryable<ProjectTeam> teamQuery = _db.ProjectTeam.Include(t => t.ProjectRole);

            // if (projectId.HasValue) {
            //     teamQuery = teamQuery.Where(t => t.ProjectId == projectId.Value);
            // } else {
            //     teamQuery = teamQuery.Include(t => t.Project).Where(t => t.Project.Slug == slug);
            // }

            // teamQuery = teamQuery.OrderByDescending(t => t.DateJoined);

            // var count = await teamQuery.CountAsync();
            // var team = await teamQuery.Skip(offset.HasValue ? offset.Value : 0).Take(limit.HasValue ? limit.Value : 50).ToListAsync();
            // var results = team.Select(t => new ProjectTeamDto(t));
            // return Ok(new { count, results });

            return Ok();
        }

        [HttpPost("{projectId:int}/quick-links")]
        [HttpPost("{slug:regex([[\\w-]])}/quick-links")]
        public async Task<IActionResult> CreateQuickLink() {
            return Ok();
        }

        [HttpPut("{projectId:int}/quick-links/{linkId:int}")]
        [HttpPut("{slug:regex([[\\w-]])}/quick-links/{linkId:int}")]
        public async Task<IActionResult> UpdateQuickLink() {
            return Ok();
        }

        [HttpDelete("{projectId:int}/quick-links/{linkId:int}")]
        [HttpDelete("{slug:regex([[\\w-]])}/quick-links/{linkId:int}")]
        public async Task<IActionResult> DeleteQuickLink() {
            return Ok();
        }

        [HttpDelete("{projectId:int}/quick-links")]
        [HttpDelete("{slug:regex([[\\w-]])}/quick-links")]
        public async Task<IActionResult> DeleteQuickLinks() {
            return Ok();
        }
        #endregion


        private async Task<Projects> GetProject(int? id, string slug) {
            IQueryable<Projects> projectQuery = _db.Projects.Include(p => p.QuickLinksHousingProject);
            Projects project;

            if (id.HasValue) {
                project = await projectQuery.SingleOrDefaultAsync(p => p.ProjectId == id.Value);
            } else {
                project = await projectQuery.SingleOrDefaultAsync(p => p.Slug == slug);
            }

            return project;
        }
    }
}
