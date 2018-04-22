using System.Security.Cryptography;
using System.Text;

namespace kms.Services
{
    public class Md5HashService : IMd5HashService
    {
        public string GetHash(string input)
        {
            MD5 md5 = MD5.Create();
            byte[] hash = md5.ComputeHash(System.Text.Encoding.ASCII.GetBytes(input));
            StringBuilder pwd = new StringBuilder();
            for (int i = 0; i < hash.Length; i++)
                pwd.Append(hash[i].ToString("x2"));
            return pwd.ToString();
        }
    }
}
