using System.Collections.Generic;
using System.Threading.Tasks;
using kms.Models;

namespace kms.Repository
{
    public interface IUserRepository : ICrudRepository<User>
    {
        Task<User> GetByEmailAndPassword(string email, string password);
        string CreateToken(User user);
    }
}
