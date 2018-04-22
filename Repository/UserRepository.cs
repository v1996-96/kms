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
            => await db.QuerySingleOrDefaultAsync<User>(
                @"SELECT * FROM users WHERE email = @email AND password = @password",
                new { email = email, password = md5.GetHash(password) });

        public async Task<User> GetByEmail(string email)
            => await db.QuerySingleOrDefaultAsync<User>(@"SELECT * FROM users WHERE email = @email", new { email = email });

        public async Task Add(User user)
        {
            await db.ExecuteAsync(@"insert into users (name, surname, email, password, avatar) values (@Name, @Surname, @Email, @Password, @Avatar)", user);
        }

        public async Task<User> GetById(int id)
            => await db.QuerySingleOrDefaultAsync<User>(@"SELECT * FROM users WHERE user_id = @id", new { id = id });

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
