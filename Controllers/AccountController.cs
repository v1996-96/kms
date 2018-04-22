using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using kms.Dtos;
using kms.Models;
using kms.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace kms.Controllers
{
    [Route("api/[controller]"), Authorize]
    public class AccountController : Controller
    {
        private readonly IAccountRepository _accountRepository;

        public AccountController(IAccountRepository accountRepository)
        {
            this._accountRepository = accountRepository;
        }

        [HttpPost("signin")]
        [AllowAnonymous]
        public async Task<IActionResult> SignIn([FromBody] SigninDto model)
            => Ok(await _accountRepository.SignIn(model.Email, model.Password));

        [HttpPost("signup")]
        [AllowAnonymous]
        public async Task<IActionResult> SignUp([FromBody] User user)
            => Ok(await _accountRepository.SignUp(user));

        [HttpPost("tokens/{token}/refresh")]
        public async Task<IActionResult> RefreshAccessToken(string token)
            => Ok(await _accountRepository.RefreshAccessToken(token));

        [HttpPost("tokens/{token}/revoke")]
        public async Task<IActionResult> RevokeAccessToken(string token) {
            await _accountRepository.RevokeRefreshToken(token);
            return Ok();
        }
    }
}
