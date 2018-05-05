using System;
using System.Collections.Generic;
using DomainObjects.Interfaces.Handlers;
using DomainObjects.Models;

namespace BLL.Handlers
{
    
    public class ChatHandler:IChatHandler
    {
        private const int CacheCapacity = 50;
        private readonly Queue<ChatMessage> cacheMessage = new Queue<ChatMessage>(CacheCapacity);

        public ChatHandler()
        {
            // Here I might need to fill the cache if the server restarts
            
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
            
            return chat;
        }

        public IEnumerable<ChatMessage> GetCacheMessages()
        {
            return cacheMessage;
        }
    }
}