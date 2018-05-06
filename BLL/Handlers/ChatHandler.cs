using System;
using System.Collections.Generic;
using DomainObjects.Interfaces.Handlers;
using DomainObjects.Interfaces.Repositories;
using DomainObjects.Models;

namespace BLL.Handlers
{
    
    public class ChatHandler:IChatHandler
    {
        private readonly IMessageRepository _messageRepository;
        private const int CacheCapacity = 50;
        private readonly Queue<ChatMessage> cacheMessage = new Queue<ChatMessage>(CacheCapacity);
        
        public ChatHandler(IMessageRepository messageRepository)
        {
            _messageRepository = messageRepository;
            foreach (var retrieveLastMessage in messageRepository.RetrieveLastMessages())
            {
                cacheMessage.Enqueue(retrieveLastMessage);    
            }
        }

        public ChatMessage SendMessage(ChatMessage chat)
        {
            chat.id = Guid.NewGuid();
            chat.messageTime = DateTime.Now;
            if (cacheMessage.Count == CacheCapacity)
            {
                cacheMessage.Dequeue();
            }
            cacheMessage.Enqueue(chat);
            
            // TODO : PERSIST IT TO A DATASTORE ( maybe using a repository pattern ? )
            _messageRepository.InsertMessage(chat);
            
            return chat;
        }

        public IEnumerable<ChatMessage> GetCacheMessages()
        {
            return cacheMessage;
        }
    }
}