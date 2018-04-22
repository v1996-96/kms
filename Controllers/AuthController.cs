using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using kms.Models;
using kms.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace kms.Controllers
{
    [Route("api")]
    public class AuthController : Controller
    {
        private readonly IUserRepository _users;

        public AuthController(IUserRepository userRepository)
        {
            this._users = userRepository;
        }

        public class LoginDTO {
            public string Email { get; set; }
            public string Password { get; set; }
        }
        [HttpPost("signin")]
        public async Task<IActionResult> Login([FromBody] LoginDTO model) {
            var user = await _users.GetByEmailAndPassword(model.Email, model.Password);
            if (user != null) {
                var token = _users.CreateToken(user);
                return Ok(new { token = token });
            } else {
                return BadRequest(new { message = "Wrong email or password" });
            }
        }

        [HttpPost("signup")]
        public async Task<IActionResult> Register([FromBody] User user) {
            await _users.Add(user);
            return Ok("Registered");
        }

        [HttpGet("profile/{id:int}"), Authorize]
        public async Task<IActionResult> Profile(int id) {
            return Ok("You have access");
        }
    }
}
