using System;
using System.Collections.Generic;
using System.Data.Common;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using kms.Data;
using kms.Models;
using kms.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace kms.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly DbConnection db;
        private readonly IMd5HashService md5;
        private readonly IConfiguration configuration;

        public UserRepository(IKMSDBConnection kmsdb, IMd5HashService md5, IConfiguration configuration)
        {
            this.db = kmsdb.Connection;
            this.md5 = md5;
            this.configuration = configuration;
        }

        public async Task<User> GetByEmailAndPassword(string email, string password)
        {
            var user = await db.QueryAsync<User>(
                @"SELECT * FROM users WHERE email = @email AND password = @password LIMIT 1",
                new { email = email, password = md5.GetHash(password) });

            return user == null ? null : user.FirstOrDefault();
        }

        public string CreateToken(User user) {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                configuration["Jwt:Issuer"],
                configuration["Jwt:Issuer"],
                expires: DateTime.Now.AddMinutes(30),
                signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public async Task Add(User user)
        {
            user.Password = md5.GetHash(user.Password);
            await db.ExecuteAsync(@"insert into users (user_id, name, surname, email, password, avatar) values (@UserId, @Name, @Surname, @Email, @Password, @avatar)", user);
        }

        public Task<User> GetById(int id)
        {
            throw new System.NotImplementedException();
        }

        public Task<IEnumerable<User>> GetList()
        {
            throw new System.NotImplementedException();
        }

        public Task Remove(int id)
        {
            throw new System.NotImplementedException();
        }

        public Task Update(User item)
        {
            throw new System.NotImplementedException();
        }
    }
}
