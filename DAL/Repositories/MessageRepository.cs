using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DAL.Database;
using DomainObjects.Interfaces.Repositories;
using DomainObjects.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace DAL.Repositories
{
    public class MessageRepository : IMessageRepository
    {
        
        private readonly MessageContext _context = null;

        public MessageRepository(IOptions<ConnectionSettings> settings)
        {
            _context = new MessageContext(settings);
        }
        
        public async Task InsertMessage(ChatMessage chat)
        {
            await _context.Messages.InsertOneAsync(chat);
        }

        public IEnumerable<ChatMessage> RetrieveLastMessages()
        {
            var cacheMessages = _context.Messages.Find(_ => true).ToList().OrderBy(c => c.messageTime).Take(40);
            return cacheMessages;
        }
    }
}