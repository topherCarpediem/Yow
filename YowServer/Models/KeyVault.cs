using System;

namespace Yow.YowServer.Models
{
    public class KeyVault
    {
        public Guid Id { get; set; }
        public byte[] SecretKey { get; set; }
        public byte[] PublicKey { get; set; }


        public Guid UserId { get; set; }
        public User User { get; set; }
    }
}