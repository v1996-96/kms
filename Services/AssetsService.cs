using System;
using System.IO;
using System.Threading.Tasks;
using kms.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;

namespace kms.Services
{
    public class AssetsService : IAssetsService
    {
        private readonly IHostingEnvironment _env;
        private readonly IMd5HashService _hasher;
        public AssetsService(IHostingEnvironment env, IMd5HashService hasher)
        {
            this._hasher = hasher;
            this._env = env;

        }
        public async Task<FileModel> SaveFile(IFormFile file, string dirname)
        {
            if (file == null)
            {
                throw new Exception("File not provided");
            }

            string extension = Path.GetExtension(file.FileName);
            string path = Path.Combine(dirname, String.Format("{0}_{1}{2}", _hasher.RandomString(20), DateTime.UtcNow.Ticks, extension));
            using (var fileStream = new FileStream(Path.Combine(_env.WebRootPath, path), FileMode.Create)) {
                await file.CopyToAsync(fileStream);
            }

            return new FileModel{
                FileName = file.FileName,
                Path = path,
                Extension = extension
            };
        }
    }
}
