using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Yow.YowServer.Models
{
    public class Message
    {
        public Guid Id { get; set; }
        public byte[] Content { get; set; }
        public DateTime DateCreated { get; set; }

        public Guid SenderId { get; set; }
        public User Sender { get; set; }
        public Guid RecieverId { get; set; }
        public User Reciever { get; set; }
    }
}