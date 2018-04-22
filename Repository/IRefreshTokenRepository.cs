using System.Threading.Tasks;
using kms.Models;

namespace kms.Repository
{
    public interface IRefreshTokenRepository : ICrudRepository<RefreshToken>
    {
        Task<RefreshToken> GetByTokenString(string token);
    }
}
