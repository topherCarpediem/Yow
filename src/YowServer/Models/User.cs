using System;
using System.Collections.Generic;

namespace Yow.YowServer.Models
{
    public class User
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }


        public KeyVault KeyVault { get; set; }
        public ICollection<Message> SenderMessages { get; set; }
        public ICollection<Message> RecieverMessages { get; set; }
    }
}