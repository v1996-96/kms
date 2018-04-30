using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using kms.Data;
using kms.Data.Entities;
using kms.Models;
using kms.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace kms.Controllers
{
    [Route("api/[controller]"), Authorize]
    public class UsersController : Controller
    {
        private int UserId { get { return int.Parse(User.Identity.Name); } }
        private readonly KMSDBContext _db;
        private readonly IPasswordHasher<Users> _passwordHasher;

        public UsersController(KMSDBContext context, IPasswordHasher<Users> passwordHasher)
        {
            this._db = context;
            this._passwordHasher = passwordHasher;
        }

        [HttpGet]
        public async Task<IActionResult> GetList(int? offset, int? limit) {
            var usersQuery =  _db.Users.OrderByDescending(u => u.UserId);
            var count = await usersQuery.CountAsync();
            var users = await usersQuery.Skip(offset.HasValue ? offset.Value : 0).Take(limit.HasValue ? limit.Value : 50).ToListAsync();
            var results = users.Select(u => new UserShortDto(u));
            return Ok(new { count, results });
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetSingle([FromRoute] int id) {
            var user = await GetById(id);

            if (user == null) {
                return NotFound();
            }

            return Ok(new UserDto(user));
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UserDto updatedUser) {
            if (updatedUser == null) {
                return BadRequest();
            }

            var user = await GetById(id);
            if (user == null) {
                return NotFound();
            }

            // If email was changed
            if (updatedUser.Email != user.Email) {
                var userByEmail = await _db.Users.SingleOrDefaultAsync(u => u.Email == updatedUser.Email);
                if (userByEmail != null) {
                    throw new Exception("Username with provided email already exists.");
                }
                user.Email = updatedUser.Email;
            }

            user.Name = updatedUser.Name;
            user.Surname = updatedUser.Surname;
            user.Avatar = updatedUser.Avatar;

            if (user.UserCompetences == null) {
                user.UserCompetences = new List<UserCompetences>();
            }
            if (user.UserRoles == null) {
                user.UserRoles = new List<UserRoles>();
            }

            if (updatedUser.Competences != null) {
                var toDelete = user.UserCompetences.Where(uc => !updatedUser.Competences.Select(c => c.CompetenceId).Contains(uc.CompetenceId)).Select(uc => uc.CompetenceId);
                var toAdd = updatedUser.Competences.Where(u => !user.UserCompetences.Select(uc => uc.CompetenceId).Contains(u.CompetenceId)).Select(c => c.CompetenceId);
                _db.UserCompetences.RemoveRange(_db.UserCompetences.Where(uc => toDelete.Contains(uc.CompetenceId)));
                var existingCompetencesToAdd = await _db.Competences.Where(uc => toAdd.Contains(uc.CompetenceId)).ToListAsync();
                _db.UserCompetences.AddRange(existingCompetencesToAdd.Select(c => new UserCompetences{ UserId = id, CompetenceId = c.CompetenceId }));
            }

            if (updatedUser.Roles != null) {
                var toDelete = user.UserRoles.Where(uc => !updatedUser.Roles.Select(c => c.RoleId).Contains(uc.RoleId)).Select(uc => uc.RoleId);
                var toAdd = updatedUser.Roles.Where(u => !user.UserRoles.Select(uc => uc.RoleId).Contains(u.RoleId)).Select(c => c.RoleId);
                _db.UserRoles.RemoveRange(_db.UserRoles.Where(uc => toDelete.Contains(uc.RoleId)));
                var existingRolesToAdd = await _db.Roles.Where(uc => toAdd.Contains(uc.RoleId)).ToListAsync();
                _db.UserRoles.AddRange(existingRolesToAdd.Select(c => new UserRoles{ UserId = id, RoleId = c.RoleId }));
            }

            await _db.SaveChangesAsync();

            return Ok(new UserDto(user));
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteSingle([FromRoute] int id) {
            var user = await _db.Users.SingleOrDefaultAsync(u => u.UserId == id);

            if (user == null) {
                return NotFound();
            }

            _db.Users.Remove(user);
            await _db.SaveChangesAsync();
            return Ok();
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteMultiple([FromQuery] int[] ids) {
            _db.Users.RemoveRange(_db.Users.Where(u => ids.Contains(u.UserId)));
            await _db.SaveChangesAsync();
            return Ok();
        }

        [HttpPost("invite")]
        public async Task<IActionResult> Invite([FromBody] InviteCreateDto invite) {
            var userByEmail = await _db.Users.SingleOrDefaultAsync(u => u.Email == invite.Email);

            if (userByEmail != null) {
                throw new Exception("User with provided email is already registered");
            }

            var existingToken = await _db.InviteTokens.SingleOrDefaultAsync(t => t.Email == invite.Email);
            if (existingToken != null) {
                existingToken.TimeCreated = DateTime.UtcNow;
                await _db.SaveChangesAsync();
                return Ok(existingToken);
            }

            var inviteToken = new InviteTokens{
                Token = _passwordHasher.HashPassword(new Users{ Email = invite.Email }, Guid.NewGuid().ToString()).Replace("+", string.Empty).Replace("=", string.Empty).Replace("/", string.Empty),
                Email = invite.Email,
                TimeCreated = DateTime.UtcNow
            };
            _db.InviteTokens.Add(inviteToken);
            await _db.SaveChangesAsync();
            return Ok(inviteToken);
        }

        private async Task<Users> GetById(int id)
            => await _db.Users
                .Include(u => u.UserRoles).ThenInclude(r => r.Role)
                .Include(u => u.UserCompetences).ThenInclude(c => c.Competence)
                .SingleOrDefaultAsync(u => u.UserId == id);
    }
}
