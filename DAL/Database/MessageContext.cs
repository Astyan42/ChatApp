using DomainObjects.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace DAL.Database
{
    public class MessageContext
    {
            private readonly IMongoDatabase _database = null;

            public MessageContext(IOptions<ConnectionSettings> settings)
            {
                var client = new MongoClient(settings.Value.ConnectionString);
                _database = client.GetDatabase(settings.Value.Database);
            }

            public IMongoCollection<ChatMessage> Messages
            {
                get
                {
                    return _database.GetCollection<ChatMessage>("Messages");
                }
            }
        }
    
}