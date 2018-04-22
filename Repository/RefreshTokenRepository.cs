using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using kms.Data;
using kms.Models;

namespace kms.Repository
{
    public class RefreshTokenRepository : IRefreshTokenRepository
    {
        private readonly DbConnection db;
        public RefreshTokenRepository(IKMSDBConnection kmsdb)
        {
            this.db = kmsdb.Connection;
        }

        public async Task Add(RefreshToken item)
        {
            await db.ExecuteAsync(@"insert into refresh_tokens (user_id, token, revoked, time_created) values (@UserId, @Token, @Revoked, @TimeCreated)", item);
        }

        public Task<RefreshToken> GetById(int id)
        {
            throw new System.NotImplementedException();
        }

        public async Task<RefreshToken> GetByTokenString(string token)
            => await db.QuerySingleOrDefaultAsync<RefreshToken>(@"SELECT * FROM refresh_tokens WHERE token = @token", new { token = token });

        public Task<IEnumerable<RefreshToken>> GetList()
        {
            throw new System.NotImplementedException();
        }

        public Task Remove(int id)
        {
            throw new System.NotImplementedException();
        }

        public async Task Update(RefreshToken item)
        {
            await db.ExecuteAsync(@"update refresh_tokens set revoked = @Revoked where refresh_token_id = @RefreshTokenId", item);
        }
    }
}
