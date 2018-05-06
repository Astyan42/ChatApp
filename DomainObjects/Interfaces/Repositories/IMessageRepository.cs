using System.Collections.Generic;
using System.Threading.Tasks;
using DomainObjects.Models;

namespace DomainObjects.Interfaces.Repositories
{
    public interface IMessageRepository
    {
        Task InsertMessage(ChatMessage chat);
        IEnumerable<ChatMessage> RetrieveLastMessages();
    }
}