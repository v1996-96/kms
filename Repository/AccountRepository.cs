using System;
using System.Linq;
using System.Threading.Tasks;
using kms.Data;
using kms.Data.Entities;
using kms.Models;
using kms.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace kms.Repository
{
    public class AccountRepository : IAccountRepository
    {
        private readonly KMSDBContext _db;
        private readonly IPasswordHasher<Users> _passwordHasher;
        private readonly IJwtHandlerService _jwtHandler;

        public AccountRepository(
            KMSDBContext context,
            IJwtHandlerService jwtHandler,
            IPasswordHasher<Users> passwordHasher)
        {
            this._jwtHandler = jwtHandler;
            this._db = context;
            this._passwordHasher = passwordHasher;
        }

        public async Task<Jwt> RefreshAccessToken(string token)
        {
            var refreshToken = await _db.RefreshTokens.SingleOrDefaultAsync(t => t.Token == token);
            if (refreshToken == null)
            {
                throw new Exception("Refresh token was not found.");
            }
            if (refreshToken.Revoked)
            {
                throw new Exception("Refresh token was revoked");
            }

            var user = await _db.Users.SingleOrDefaultAsync(u => u.UserId == refreshToken.UserId);
            var jwt = _jwtHandler.Create(user);
            jwt.RefreshToken = refreshToken.Token;

            return jwt;
        }

        public async Task RevokeRefreshToken(string token)
        {
            var refreshToken = await _db.RefreshTokens.SingleOrDefaultAsync(t => t.Token == token);
            if (refreshToken == null)
            {
                throw new Exception("Refresh token was not found.");
            }

            var refreshTokens = await _db.RefreshTokens.Where(t => t.UserId == refreshToken.UserId).ToListAsync();

            refreshTokens.ForEach(t => t.Revoked = true);
            await _db.SaveChangesAsync();
        }

        public async Task<Jwt> SignIn(string email, string password)
        {
            var user = await _db.Users.SingleOrDefaultAsync(u => u.Email == email);
            if (user == null) {
                throw new Exception("Wrong email");
            }

            var comparisonResult = _passwordHasher.VerifyHashedPassword(user, user.Password, password);
            if (comparisonResult == PasswordVerificationResult.Failed) {
                throw new Exception("Wrong password");
            }

            var jwt = _jwtHandler.Create(user);
            var refreshToken = new RefreshTokens{
                Token = _passwordHasher.HashPassword(user, Guid.NewGuid().ToString()).Replace("+", string.Empty).Replace("=", string.Empty).Replace("/", string.Empty),
                UserId = user.UserId,
                Revoked = false,
                TimeCreated = DateTime.Now
            };

            _db.RefreshTokens.Add(refreshToken);
            await _db.SaveChangesAsync();
            jwt.RefreshToken = refreshToken.Token;


            return jwt;
        }

        public async Task<Jwt> SignUp(Users user)
        {
            var userByEmail = await _db.Users.SingleOrDefaultAsync(u => u.Email == user.Email);
            if (userByEmail != null) {
                throw new Exception("Username with provided email already exists.");
            }

            var unhahshedPassword = user.Password;
            user.Password = _passwordHasher.HashPassword(user, user.Password);
            _db.Users.Add(user);
            await _db.SaveChangesAsync();

            return await SignIn(user.Email, unhahshedPassword);
        }
    }
}
