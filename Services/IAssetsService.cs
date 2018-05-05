using System.Threading.Tasks;
using kms.Models;
using Microsoft.AspNetCore.Http;

namespace kms.Services
{
    public interface IAssetsService
    {
        Task<FileModel> SaveFile(IFormFile file, string dirname);
    }
}
