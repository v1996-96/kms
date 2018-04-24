using kms.Data.Entities;
using kms.Models;

namespace kms.Services
{
    public interface IJwtHandlerService
    {
         Jwt Create(Users user);
    }
}
