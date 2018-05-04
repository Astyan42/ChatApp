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
        
        /// <summary>
        /// POST Service
        /// This service will log a new user into the chat.
        /// This will send the join requested along with the socket id and send the message list and connected user through the socket
        /// </summary>
        /// <param name="userJoining">the user joining the chat. An id will be attributed to the user and he will be sent back to the client</param>
        /// <param name="socketId">the socketId of the requester</param>
        [HttpPost]
        public async void Join([FromBody]User userJoining, [FromQuery]string socketId)
        {
            Console.WriteLine("UserController : Join");
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
        
        /// <summary>
        /// GET Service
        /// This service will return a list of connected user.
        /// It will send the list back using the websocket
        /// </summary>
        /// <param name="socketId">The id of the socket which will be used to send the answer</param>
        [HttpGet]
        public async void GetList(string socketId)
        {
            Console.WriteLine("UserController : GetList");
            var mess = _notificationsMessageHandler.GenerateMessageFromPayload(_userHandler.GetUsers(), MessageType.UsersRequested);
            await _notificationsMessageHandler.SendMessageAsync(socketId, mess);
        }
    }
}