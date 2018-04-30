using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using kms.Data;
using kms.Data.Entities;
using kms.Models;
using kms.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace kms.Controllers
{
    [Route("api/[controller]"), Authorize]
    public class ProfileController : Controller
    {
        private readonly IUserRepository _userRepository;
        public ProfileController(IUserRepository userRepository)
        {
            this._userRepository = userRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetProfile()
            => Ok(await _userRepository.GetProfile(int.Parse(User.Identity.Name)));
    }
}
