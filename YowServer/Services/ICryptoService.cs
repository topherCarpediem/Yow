using System.Collections.Generic;
using System.Threading.Tasks;

namespace Yow.YowServer.Services
{
    public interface ICryptoService
    {
        Dictionary<string, byte[]> GenerateKeyValuePair();

        string SecurePassword(string plainPassword);
        bool VerifyPassword(string hashPassword, string plainPassword);
    }
}