using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using kms.Data.Entities;
using kms.Models;
using kms.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using kms.Utils;

namespace kms.Controllers
{
    [KmsController, Authorize]
    public class AuthController : Controller
    {
        private readonly IAccountRepository _accountRepository;
        public AuthController(IAccountRepository accountRepository)
        {
            this._accountRepository = accountRepository;
        }

        [HttpPost("signin")]
        [AllowAnonymous]
        public async Task<IActionResult> SignIn([FromBody] SigninDto model)
            => Ok(await _accountRepository.SignIn(model.Email, model.Password));

        [HttpPost("signup")]
        [AllowAnonymous]
        public async Task<IActionResult> SignUp([FromBody] Users user)
            => Ok(await _accountRepository.SignUp(user));

        [HttpPost("refresh/{token}")]
        public async Task<IActionResult> RefreshAccessToken([FromRoute] string token)
            => Ok(await _accountRepository.RefreshAccessToken(token));

        [HttpPost("revoke/{token}")]
        public async Task<IActionResult> RevokeRefreshToken([FromRoute] string token) {
            await _accountRepository.RevokeRefreshToken(token);
            return Ok();
        }

        [HttpPost("revokeall/{token}")]
        public async Task<IActionResult> RevokeAllRefreshTokens([FromRoute] string token) {
            await _accountRepository.RevokeAllRefreshTokens(token);
            return Ok();
        }
    }
}
