using System;
using System.Threading.Tasks;
using kms.Dtos;
using kms.Models;
using kms.Services;
using Microsoft.AspNetCore.Identity;

namespace kms.Repository
{
    public class AccountRepository : IAccountRepository
    {
        private readonly IUserRepository _userRepository;
        private readonly IRefreshTokenRepository _refreshTokenRepository;
        private readonly IPasswordHasher<User> _passwordHasher;
        private readonly IJwtHandlerService _jwtHandler;

        public AccountRepository(
            IUserRepository userRepository,
            IRefreshTokenRepository refreshTokenRepository,
            IJwtHandlerService jwtHandler,
            IPasswordHasher<User> passwordHasher)
        {
            this._jwtHandler = jwtHandler;
            this._userRepository = userRepository;
            this._refreshTokenRepository = refreshTokenRepository;
            this._passwordHasher = passwordHasher;
        }

        public async Task<Jwt> RefreshAccessToken(string token)
        {
            var refreshToken = await _refreshTokenRepository.GetByTokenString(token);
            if (refreshToken == null)
            {
                throw new Exception("Refresh token was not found.");
            }
            if (refreshToken.Revoked)
            {
                throw new Exception("Refresh token was revoked");
            }

            var user = await _userRepository.GetById(refreshToken.UserId);
            var jwt = _jwtHandler.Create(user);
            jwt.RefreshToken = refreshToken.Token;

            return jwt;
        }

        public async Task RevokeRefreshToken(string token)
        {
            var refreshToken = await _refreshTokenRepository.GetByTokenString(token);
            if (refreshToken == null)
            {
                throw new Exception("Refresh token was not found.");
            }
            if (refreshToken.Revoked)
            {
                throw new Exception("Refresh token was revoked");
            }

            refreshToken.Revoked = true;
            await _refreshTokenRepository.Update(refreshToken);
        }

        public async Task<Jwt> SignIn(string email, string password)
        {
            var user = await _userRepository.GetByEmail(email);
            if (user == null) {
                throw new Exception("Wrong email");
            }

            var comparisonResult = _passwordHasher.VerifyHashedPassword(user, user.Password, password);
            if (comparisonResult == PasswordVerificationResult.Failed) {
                throw new Exception("Wrong password");
            }

            var jwt = _jwtHandler.Create(user);
            var refreshToken = new RefreshToken{
                Token = _passwordHasher.HashPassword(user, Guid.NewGuid().ToString()).Replace("+", string.Empty).Replace("=", string.Empty).Replace("/", string.Empty),
                UserId = user.UserId,
                Revoked = false,
                TimeCreated = DateTime.Now
            };

            await _refreshTokenRepository.Add(refreshToken);
            jwt.RefreshToken = refreshToken.Token;

            return jwt;
        }

        public async Task<Jwt> SignUp(User user) {
            var userByEmail = await _userRepository.GetByEmail(user.Email);
            if (userByEmail != null) {
                throw new Exception("Username with provided email already exists.");
            }

            var unhahshedPassword = user.Password;
            user.Password = _passwordHasher.HashPassword(user, user.Password);
            await _userRepository.Add(user);

            return await SignIn(user.Email, unhahshedPassword);
        }
    }
}
