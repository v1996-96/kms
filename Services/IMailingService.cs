using System.Threading.Tasks;
using kms.Data.Entities;
using kms.Models;

namespace kms.Services
{
    public interface IMailingService
    {
        Task SendAsync(string email, string subject, string message);
    }
}
