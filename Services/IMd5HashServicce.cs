using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace kms.Services
{
    public interface IMd5HashService
    {
        string RandomString(int length);

        string GetHash(string input);
    }
}

