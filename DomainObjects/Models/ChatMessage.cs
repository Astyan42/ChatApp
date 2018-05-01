using System;

namespace DomainObjects.Models
{
    public class ChatMessage
    {
        public Guid id { get; set; }
        
        public User sender { get; set; }
        
        public string content { get; set; }
        
        public DateTime messageTime { get; set; }
    }
}