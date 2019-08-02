using System.Collections.Generic;
using System.Linq;
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

            CngKey cngKey = CngKey.Create(CngAlgorithm.ECDiffieHellmanP256, null, new CngKeyCreationParameters { ExportPolicy = CngExportPolicies.AllowPlaintextExport });

            using (ECDiffieHellmanCng ecdh = new ECDiffieHellmanCng(cngKey))
            {
                publicKey = ecdh.Key.Export(CngKeyBlobFormat.EccPublicBlob);
                secretKey = ecdh.Key.Export(CngKeyBlobFormat.EccPrivateBlob);
            }

            eccKeyValuePair.Add("publicKey", publicKey.Skip(8).ToArray());
            eccKeyValuePair.Add("secretKey", secretKey.Skip(publicKey.Length).ToArray());

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