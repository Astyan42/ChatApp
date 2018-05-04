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
    /// <summary>
    /// This is an override of the WebSocket Handler in order to have specific methods
    /// </summary>
    public class NotificationsMessageHandler : WebSocketHandler
    {
        /// <summary>
        /// User Handler
        /// </summary>
        private readonly IUserHandler _userHandler;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="webSocketConnectionManager"></param>
        /// <param name="_userHandler"></param>
        public NotificationsMessageHandler(WebSocketConnectionManager webSocketConnectionManager, IUserHandler _userHandler) : base(webSocketConnectionManager, new StringMethodInvocationStrategy())
        {
            this._userHandler = _userHandler;
        }

        /// <summary>
        /// Generate the message sent through the network
        /// </summary>
        /// <param name="payload">The object sent to the client. This will be serialized</param>
        /// <param name="messageType">The type of message indicating the action to achieve</param>
        /// <typeparam name="T">Type of the payload</typeparam>
        /// <returns>Message that will be sent through the websocket</returns>
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

        public override async Task OnDisconnected(WebSocket socket)
        {
            var socketId = WebSocketConnectionManager.GetId(socket);
            var deletedUser = _userHandler.Remove(socketId);
            var mess = this.GenerateMessageFromPayload(deletedUser, DomainObjects.Messages.MessageType.UserLeft);
            await base.OnDisconnected(socket);
            await this.SendMessageToAllAsync(mess);
        }
    }
}