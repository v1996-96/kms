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
using System.Data.SqlClient;
using Npgsql;

namespace kms.Controllers
{
    [KmsController, Authorize]
    public class ProjectsController : Controller
    {
        private int UserId { get { return int.Parse(User.Identity.Name); } }

        private readonly KMSDBContext _db;
        private readonly ISearchRepository _search;

        public ProjectsController(KMSDBContext context, ISearchRepository search)
        {
            this._db = context;
            this._search = search;
        }

        #region CRUD

        [HttpGet]
        public async Task<IActionResult> GetList([FromQuery] int? user, [FromQuery] bool? my, [FromQuery] int? offset, [FromQuery] int? limit, [FromQuery] string query) {
            IQueryable<Projects> projectsQuery;

            if (query.IsValidQuery()) {
                projectsQuery = _search.SearchProjects(query);
            } else {
                projectsQuery = _db.Projects.OrderByDescending(p => p.ProjectId);
            }

            if (user.HasValue) {
                projectsQuery = projectsQuery.Include(p => p.ProjectTeam).Where(p => p.ProjectTeam.Any(pt => pt.UserId == user.Value));
            } else if (my.HasValue && my.Value) {
                projectsQuery = projectsQuery.Include(p => p.ProjectTeam).Where(p => p.ProjectTeam.Any(pt => pt.UserId == UserId));
            }

            var count = await projectsQuery.CountAsync();
            var results = await projectsQuery.Skip(offset.HasValue ? offset.Value : 0).Take(limit.HasValue ? limit.Value : 50).Include(p => p.ProjectTeam).Select(p => new ProjectShortDto(p, p.ProjectTeam.Count())).ToListAsync();
            return Ok(new { count, results });
        }

        [HttpGet("{id:int}")]
        [HttpGet("{slug:regex([[\\w-]])}")]
        public async Task<IActionResult> GetSingle([FromRoute] int? id, [FromRoute] string slug) {
            var project = await GetProject(id, slug);
            if (project == null) {
                return NotFound();
            }

            var membersCount = await _db.ProjectTeam.Where(pt => pt.ProjectId == project.ProjectId).CountAsync();

            return Ok(new ProjectDto(project, membersCount));
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] ProjectCreateDto project) {
            if (project == null) {
                return BadRequest();
            }

            int attemptsCount = 0;
            int maxAttemptsCount = 100;
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
            var membersCount = await _db.ProjectTeam.Where(pt => pt.ProjectId == newProject.ProjectId).CountAsync();

            return Ok(new ProjectDto(newProject, membersCount));
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

            if (project.Name != updatedProject.Name) {
                int attemptsCount = 0;
                int maxAttemptsCount = 100;
                string newSlug;
                bool isSlugUnique = false;

                do {
                    newSlug = updatedProject.Name.GenerateSlug();
                    isSlugUnique = (await _db.Projects.SingleOrDefaultAsync(p => p.Slug == newSlug)) == null;
                } while (!isSlugUnique && attemptsCount < maxAttemptsCount);

                if (!isSlugUnique) {
                    return BadRequest(new { message = "Cannot generate unique slug" });
                }

                project.Name = updatedProject.Name;
                project.Slug = newSlug;
            }


            project.Description = updatedProject.Description;
            project.Goal = updatedProject.Goal;
            project.DateStart = updatedProject.DateStart;
            project.DateEnd = updatedProject.DateEnd;
            project.Avatar = updatedProject.Avatar;
            project.IsOpen = updatedProject.IsOpen;
            project.IsActive = updatedProject.IsActive;

            await _db.SaveChangesAsync();
            var membersCount = await _db.ProjectTeam.Where(pt => pt.ProjectId == project.ProjectId).CountAsync();

            return Ok(new ProjectDto(project, membersCount));
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
            IQueryable<ProjectTeam> teamQuery = _db.ProjectTeam.Include(t => t.ProjectRole).Include(t => t.User);

            if (projectId.HasValue) {
                teamQuery = teamQuery.Where(t => t.ProjectId == projectId.Value);
            } else {
                teamQuery = teamQuery.Include(t => t.Project).Where(t => t.Project.Slug == slug);
            }

            if (query.IsValidQuery()) {
                teamQuery = _search.SearchProjectTeam(teamQuery, query);
            } else {
                teamQuery = teamQuery.OrderByDescending(t => t.DateJoined);
            }

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

            var existingMember = await _db.ProjectTeam.SingleOrDefaultAsync(t => t.UserId == member.UserId && t.ProjectId == project.ProjectId);
            if (existingMember != null) {
                throw new Exception("User is already in project team");
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
            await _db.Entry(newMember).Reference(t => t.User).LoadAsync();

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

            var member = await _db.ProjectTeam.Include(t => t.ProjectRole).Include(t => t.User).SingleOrDefaultAsync(t => t.UserId == updatedMember.UserId && t.ProjectId == project.ProjectId);
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
            var project = await GetProject(projectId, slug);
            if (project == null) {
                return NotFound();
            }

            var quickLinksQuery = _db.QuickLinks.Include(q => q.Document).Include(q => q.Project).Where(q => q.HousingProjectId == project.ProjectId && q.UserId == UserId);

            if (query.IsValidQuery()) {
                quickLinksQuery = _search.SearchQuickLinks(quickLinksQuery, query);
            } else {
                quickLinksQuery = quickLinksQuery.OrderByDescending(q => q.QuickLinkId);
            }

            var count = await quickLinksQuery.CountAsync();
            var quickLinks = await quickLinksQuery.Skip(offset.HasValue ? offset.Value : 0).Take(limit.HasValue ? limit.Value : 50).ToListAsync();
            var results = quickLinks.Select(q => new QuickLinkShortDto(q));
            return Ok(new { count, results });
        }

        [HttpPost("{projectId:int}/quick-links")]
        [HttpPost("{slug:regex([[\\w-]])}/quick-links")]
        public async Task<IActionResult> CreateQuickLink([FromRoute] int? projectId, [FromRoute] string slug, [FromBody] QuickLinkCreateDto quickLink) {
            if (quickLink == null) {
                return BadRequest();
            }

            var project = await GetProject(projectId, slug);
            if (project == null) {
                return NotFound();
            }

            var newQuickLink = new QuickLinks{
                HousingProjectId = project.ProjectId,
                Name = quickLink.Name,
                ProjectId = quickLink.ProjectId,
                DocumentId = quickLink.DocumentId,
                UserId = UserId,
                ExternalLink = quickLink.ExternalLink
            };

            _db.QuickLinks.Add(newQuickLink);
            await _db.SaveChangesAsync();
            await _db.Entry(newQuickLink).Reference(q => q.Document).LoadAsync();
            await _db.Entry(newQuickLink).Reference(q => q.Project).LoadAsync();
            return Ok(new QuickLinkShortDto(newQuickLink));
        }

        [HttpPut("{projectId:int}/quick-links/{linkId:int}")]
        [HttpPut("{slug:regex([[\\w-]])}/quick-links/{linkId:int}")]
        public async Task<IActionResult> UpdateQuickLink([FromRoute] int? projectId, [FromRoute] string slug, [FromRoute] int linkId, [FromBody] QuickLinkShortDto quickLink) {
            var project = await GetProject(projectId, slug);
            if (project == null) {
                return NotFound();
            }

            var dbQuickLink = await _db.QuickLinks.Include(q => q.Project).Include(q => q.Document).SingleOrDefaultAsync(q => q.QuickLinkId == linkId);
            if (dbQuickLink == null) {
                return NotFound();
            }

            dbQuickLink.Name = quickLink.Name;
            dbQuickLink.ProjectId = quickLink.ProjectId;
            dbQuickLink.DocumentId = quickLink.DocumentId;
            dbQuickLink.ExternalLink = quickLink.ExternalLink;

            await _db.SaveChangesAsync();
            await _db.Entry(dbQuickLink).Reference(q => q.Document).LoadAsync();
            await _db.Entry(dbQuickLink).Reference(q => q.Project).LoadAsync();

            return Ok(new QuickLinkShortDto(dbQuickLink));
        }

        [HttpDelete("{projectId:int}/quick-links/{linkId:int}")]
        [HttpDelete("{slug:regex([[\\w-]])}/quick-links/{linkId:int}")]
        public async Task<IActionResult> DeleteQuickLink([FromRoute] int? projectId, [FromRoute] string slug, [FromRoute] int linkId) {
            var project = await GetProject(projectId, slug);
            if (project == null) {
                return NotFound();
            }

            var quickLink = await _db.QuickLinks.SingleOrDefaultAsync(q => q.QuickLinkId == linkId);
            if (quickLink == null) {
                return NotFound();
            }

            _db.QuickLinks.Remove(quickLink);
            await _db.SaveChangesAsync();
            return Ok();
        }

        [HttpDelete("{projectId:int}/quick-links")]
        [HttpDelete("{slug:regex([[\\w-]])}/quick-links")]
        public async Task<IActionResult> DeleteQuickLinks([FromRoute] int? projectId, [FromRoute] string slug, [FromQuery] int[] ids) {
            var project = await GetProject(projectId, slug);
            if (project == null) {
                return NotFound();
            }

            _db.QuickLinks.RemoveRange(_db.QuickLinks.Where(q => ids.Contains(q.QuickLinkId) && q.HousingProjectId == project.ProjectId));
            await _db.SaveChangesAsync();
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
