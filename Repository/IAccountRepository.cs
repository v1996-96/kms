using System.Threading.Tasks;
using kms.Data.Entities;
using kms.Models;

namespace kms.Repository
{
    public interface IAccountRepository
    {
        Task<Jwt> SignUp(Users user, string token);
        Task<Jwt> SignIn(string email, string password);
        Task<Jwt> RefreshAccessToken(string token);
        Task RevokeRefreshToken(string token);
        Task RevokeAllRefreshTokens(string token);
        Task<InviteDto> VerifyInviteToken(string token);
    }
}
