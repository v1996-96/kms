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

namespace kms.Controllers
{
    [Route("api/[controller]"), Authorize]
    public class ProjectsController : Controller
    {
        private int UserId { get { return int.Parse(User.Identity.Name); } }

        private readonly KMSDBContext _db;
        public ProjectsController(KMSDBContext context)
        {
            this._db = context;
        }

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

        // [HttpGet("{id:int}")]
        // public async Task<IActionResult> GetSingle([FromRoute] int id) {

        // }
    }
}
