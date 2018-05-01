using System.Collections.Generic;
using DomainObjects.Models;

namespace DomainObjects.Interfaces.Handlers
{
    public interface IChatHandler
    {
        ChatMessage SendMessage(ChatMessage chat);
        IEnumerable<ChatMessage> GetCacheMessages();
    }
}