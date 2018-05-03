using System;
using System.Collections.Generic;
using DomainObjects.Interfaces.Handlers;
using DomainObjects.Messages.Abstract;
using DomainObjects.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using WebSocketManager.Common;
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
        
        [HttpGet]
        public async void GetList(string socketId)
        {
            Console.WriteLine("MessageController : GetList");
            var mess = _notificationsMessageHandler.GenerateMessageFromPayload(_chatHandler.GetCacheMessages(), MessageType.MessageHistory);
            await _notificationsMessageHandler.SendMessageAsync(socketId, mess);
        }
        
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