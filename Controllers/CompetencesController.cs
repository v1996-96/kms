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
    public class CompetencesController : Controller
    {
        private int UserId { get { return int.Parse(User.Identity.Name); } }

        private readonly KMSDBContext _db;
        public CompetencesController(KMSDBContext context)
        {
            this._db = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetList([FromQuery] int? offset, [FromQuery] int? limit, [FromQuery] string query) {
            var competencesQuery = _db.Competences.OrderByDescending(c => c.CompetenceId);
            var count = await competencesQuery.CountAsync();
            var following = await competencesQuery.Skip(offset.HasValue ? offset.Value : 0).Take(limit.HasValue ? limit.Value : 50).ToListAsync();
            var results = following.Select(c => new CompetenceDto(c));
            return Ok(new { count, results });
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetSingle([FromRoute] int id) {
            var competence = await _db.Competences.SingleOrDefaultAsync(c => c.CompetenceId == id);
            if (competence == null) {
                return NotFound();
            }

            return Ok(new CompetenceDto(competence));
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CompetenceCreateDto competence) {
            if (competence == null) {
                return BadRequest();
            }

            var newCompetence = new Competences{ Name = competence.Name };
            _db.Competences.Add(newCompetence);
            await _db.SaveChangesAsync();
            return Ok(new CompetenceDto(newCompetence));
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] CompetenceDto competence) {
            if (competence == null) {
                return BadRequest();
            }

            var dbCompetence = await _db.Competences.SingleOrDefaultAsync(c => c.CompetenceId == id);
            if (dbCompetence == null) {
                return NotFound();
            }

            dbCompetence.Name = competence.Name;
            await _db.SaveChangesAsync();

            return Ok(new CompetenceDto(dbCompetence));
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete([FromRoute] int id) {
            var competence = await _db.Competences.SingleOrDefaultAsync(c => c.CompetenceId == id);
            if (competence == null) {
                return NotFound();
            }

            _db.Competences.Remove(competence);
            await _db.SaveChangesAsync();
            return Ok();
        }
    }
}
