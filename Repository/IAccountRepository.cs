using System.Threading.Tasks;
using kms.Dtos;
using kms.Models;

namespace kms.Repository
{
    public interface IAccountRepository
    {
        Task<Jwt> SignUp(User user);
        Task<Jwt> SignIn(string email, string password);
        Task<Jwt> RefreshAccessToken(string token);
        Task RevokeRefreshToken(string token);
    }
}
