using System;
using System.Net.WebSockets;
using System.Threading.Tasks;
using DomainObjects.Interfaces;
using DomainObjects.Interfaces.Handlers;
using DomainObjects.Messages.Abstract;
using Newtonsoft.Json;
using WebSocketManager;
using WebSocketManager.Common;

namespace WebApplication
{
    public class NotificationsMessageHandler : WebSocketHandler
    {
        private readonly IUserHandler _userHandler;

        public NotificationsMessageHandler(WebSocketConnectionManager webSocketConnectionManager, IUserHandler _userHandler) : base(webSocketConnectionManager, new StringMethodInvocationStrategy())
        {
        }

        public Message GenerateMessageFromPayload<T>(T payload, DomainObjects.Messages.MessageType messageType)
        {
            Message<T> message = new Message<T>()
            {
                type = messageType,
                payload = payload
            };

            string serializedMessage = JsonConvert.SerializeObject(message);
            
            
            var mess  = new Message()
            {
                MessageType = MessageType.Text,
                Data = serializedMessage
            };
            return mess;
        }
    }
}