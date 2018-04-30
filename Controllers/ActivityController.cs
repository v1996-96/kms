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
    public class ActivityController : Controller
    {
        private int UserId { get { return int.Parse(User.Identity.Name); } }
        private readonly KMSDBContext _db;
        public ActivityController(KMSDBContext context) {
            this._db = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetList(int? offset, int? limit) {
            var activitiesQuery = _db.Activity
                .Include(a => a.Project)
                    .ThenInclude(p => p.ProjectTeam)
                .Include(a => a.User)
                .Where(a => a.Project.ProjectTeam.All(pt => pt.UserId == UserId))
                .OrderByDescending(a => a.TimeFired);

            var count = await activitiesQuery.CountAsync();
            var activities = await activitiesQuery.Skip(offset.HasValue ? offset.Value : 0).Take(limit.HasValue ? limit.Value : 50).ToListAsync();
            var results = activities.Select(a => new ActivityDto(a));

            return Ok(new { count, results });
        }
    }
}
