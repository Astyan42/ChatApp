using System;
using System.Collections.Generic;
using DomainObjects.Interfaces;
using DomainObjects.Interfaces.Handlers;
using DomainObjects.Messages.Abstract;
using DomainObjects.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using WebSocketManager.Common;
using MessageType = DomainObjects.Messages.MessageType;

namespace WebApplication.Controllers.api.v1
{
    public class UserController : Controller
    {
        private readonly IUserHandler _userHandler;
        private readonly IChatHandler _chatHandler;
        private readonly NotificationsMessageHandler _notificationsMessageHandler;

        public UserController(NotificationsMessageHandler notificationsMessageHandler, IUserHandler userHandler, IChatHandler chatHandler)
        {
            _userHandler = userHandler;
            _chatHandler = chatHandler;
            _notificationsMessageHandler = notificationsMessageHandler;
        }
        
        [HttpPost]
        public async void Join([FromBody]User userJoining, [FromQuery]string socketId)
        {
            if(userJoining.userId == Guid.Empty) userJoining.userId = Guid.NewGuid();
            
            var mess = _notificationsMessageHandler.GenerateMessageFromPayload(userJoining, MessageType.JoinRequested);
            await _notificationsMessageHandler.SendMessageAsync(socketId, mess);
            _userHandler.Add(userJoining, socketId);

            mess = _notificationsMessageHandler.GenerateMessageFromPayload(_chatHandler.GetCacheMessages(), MessageType.MessageHistory);
            await _notificationsMessageHandler.SendMessageAsync(socketId, mess);
            
            mess = _notificationsMessageHandler.GenerateMessageFromPayload(_userHandler.GetUsers(), MessageType.UsersRequested);
            await _notificationsMessageHandler.SendMessageAsync(socketId, mess); 
            
            mess = _notificationsMessageHandler.GenerateMessageFromPayload(userJoining, MessageType.UserJoined);
            await _notificationsMessageHandler.SendMessageToAllAsync(mess);
        }
        
        [HttpGet]
        public async void GetList(string socketId)
        {
            
            var mess = _notificationsMessageHandler.GenerateMessageFromPayload(_userHandler.GetUsers(), MessageType.UsersRequested);
            await _notificationsMessageHandler.SendMessageAsync(socketId, mess);
        }
    }
}