using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using kms.Data.Entities;
using kms.Models;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace kms.Services
{
    public class JwtHandlerService : IJwtHandlerService
    {
        private readonly JwtOptions _options;
        private readonly JwtSecurityTokenHandler _jwtSecurityTokenHandler = new JwtSecurityTokenHandler();
        private readonly SecurityKey _securityKey;
        private readonly SigningCredentials _signingCredentials;
        private readonly JwtHeader _jwtHeader;

        public JwtHandlerService(IOptions<JwtOptions> options)
        {
            _options = options.Value;
            _securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_options.SecretKey));
            _signingCredentials = new SigningCredentials(_securityKey, SecurityAlgorithms.HmacSha256);
            _jwtHeader = new JwtHeader(_signingCredentials);
        }

        public Jwt Create(Users user)
        {
            var nowUtc = DateTime.UtcNow;
            var expires = nowUtc.AddMinutes(_options.ExpiryMinutes);
            var payload = new JwtPayload
            {
                {"sub", user.UserId},
                {"iss", _options.Issuer},
                {"aud", _options.Issuer},
                {"iat", DateTimeToUnixUtc(nowUtc)},
                {"exp", DateTimeToUnixUtc(expires)},
                {ClaimTypes.Name, user.UserId}
            };
            var jwt = new JwtSecurityToken(_jwtHeader, payload);
            var token = _jwtSecurityTokenHandler.WriteToken(jwt);

            return new Jwt{
                AccessToken = token
            };
        }

        private static long DateTimeToUnixUtc(DateTime time) {
            return (long)(time - new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc)).TotalSeconds;
        }
    }
}
