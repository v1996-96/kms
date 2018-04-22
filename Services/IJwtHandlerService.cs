using kms.Models;

namespace kms.Services
{
    public interface IJwtHandlerService
    {
         Jwt Create(User user);
    }
}
