using System.Collections.Generic;
using System.Security.Cryptography;
using System.Threading.Tasks;

namespace Yow.YowServer.Services
{
    public class CryptoService : ICryptoService
    {
        public Dictionary<string, byte[]> GenerateKeyValuePair()
        {
            Dictionary<string, byte[]> eccKeyValuePair = new Dictionary<string, byte[]>();

            byte[] publicKey = null;
            byte[] secretKey = null;

            using (ECDiffieHellmanCng ecdh = new ECDiffieHellmanCng())
            {
                publicKey = ecdh.Key.Export(CngKeyBlobFormat.EccPublicBlob);
                secretKey = ecdh.Key.Export(CngKeyBlobFormat.EccPrivateBlob);
            }

            eccKeyValuePair.Add("publicKey", publicKey);
            eccKeyValuePair.Add("secretKey", secretKey);

            return eccKeyValuePair;
        }

        public string SecurePassword(string plainPassword)
        {
            return BCrypt.Net.BCrypt.HashPassword(plainPassword);
        }

        public bool VerifyPassword(string hashPassword, string plainPassword)
        {
            return BCrypt.Net.BCrypt.Verify(plainPassword, hashPassword);
        }
    }
}