using System;
using DomainObjects.Interfaces.Handlers;
using DomainObjects.Models;
using Microsoft.AspNetCore.Mvc;
using MessageType = DomainObjects.Messages.MessageType;

namespace WebApplication.Controllers.api.v1
{
    public class MessageController : Controller
    {
        private readonly NotificationsMessageHandler _notificationsMessageHandler;
        private readonly IChatHandler _chatHandler;

        public MessageController(NotificationsMessageHandler notificationsMessageHandler, IChatHandler chatHandler)
        {
            _notificationsMessageHandler = notificationsMessageHandler;
            _chatHandler = chatHandler;
        }
        
        /// <summary>
        /// GET Service
        /// This service will be used in order to get back the current list of message in cache
        /// </summary>
        /// <param name="socketId">The id of the socket which will be used to send the answer</param>
        [HttpGet]
        public async void GetList(string socketId)
        {
            Console.WriteLine("MessageController : GetList");
            var mess = _notificationsMessageHandler.GenerateMessageFromPayload(_chatHandler.GetCacheMessages(), MessageType.MessageHistory);
            await _notificationsMessageHandler.SendMessageAsync(socketId, mess);
        }
        
        /// <summary>
        /// POST Service
        /// This service will be used in order to send a message to all users connected in the chat.
        /// </summary>
        /// <param name="chat">chat message should be contained in the body of the request</param>
        [HttpPost]
        public async void Send([FromBody]ChatMessage chat)
        {
            Console.WriteLine("MessageController : Send");
            chat = _chatHandler.SendMessage(chat);
            var mess = _notificationsMessageHandler.GenerateMessageFromPayload(chat, MessageType.MessageAdded);
            await _notificationsMessageHandler.SendMessageToAllAsync(mess);
        }
    }
}