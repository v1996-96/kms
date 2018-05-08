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
    public class FollowingController : Controller
    {
        private int UserId { get { return int.Parse(User.Identity.Name); } }
        private readonly ISearchRepository _search;
        private readonly KMSDBContext _db;
        public FollowingController(KMSDBContext context, ISearchRepository search)
        {
            this._search = search;
            this._db = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetList([FromQuery] int? offset, [FromQuery] int? limit, [FromQuery] string query)
        {
            IQueryable<FollowedProjects> followingQuery = _db.FollowedProjects.Include(f => f.Project).Where(f => f.UserId == UserId);

            if (query.IsValidQuery()) {
                followingQuery = _search.SearchFollowedProjects(followingQuery, query);
            } else {
                followingQuery = followingQuery.OrderByDescending(f => f.TimeCreated);
            }

            var count = await followingQuery.CountAsync();
            var following = await followingQuery.Skip(offset.HasValue ? offset.Value : 0).Take(limit.HasValue ? limit.Value : 50).ToListAsync();
            var results = following.Select(b => new FollowingDto(b));

            return Ok(new { count, results });
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetByProject([FromRoute] int id) {
            var following = await _db.FollowedProjects.Include(f => f.Project).SingleOrDefaultAsync(f => f.ProjectId == id && f.UserId == UserId);
            if (following == null) {
                return NotFound();
            }

            return Ok(new FollowingDto(following));
        }

        [HttpPost]
        public async Task<IActionResult> Follow([FromBody] FollowingCreateDto following)
        {
            if (following == null)
            {
                return BadRequest();
            }

            var project = await _db.Projects.SingleOrDefaultAsync(p => p.ProjectId == following.ProjectId);
            if (project == null)
            {
                return NotFound();
            }

            var existingFollowing = await _db.FollowedProjects.Include(f => f.Project).SingleOrDefaultAsync(f => f.ProjectId == following.ProjectId && f.UserId == UserId);
            if (existingFollowing != null)
            {
                return Ok(new FollowingDto(existingFollowing));
            }

            var newFollowing = new FollowedProjects { UserId = UserId, ProjectId = following.ProjectId, TimeCreated = DateTime.UtcNow };
            _db.FollowedProjects.Add(newFollowing);
            await _db.SaveChangesAsync();
            await _db.Entry(newFollowing).Reference(f => f.Project).LoadAsync();

            return Ok(new FollowingDto(newFollowing));
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            var following = await _db.FollowedProjects.SingleOrDefaultAsync(f => f.FollowedProjectsId == id);

            if (following == null)
            {
                return NotFound();
            }

            _db.FollowedProjects.Remove(following);
            await _db.SaveChangesAsync();

            return Ok();
        }
    }
}
