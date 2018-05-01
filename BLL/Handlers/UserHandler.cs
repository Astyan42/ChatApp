using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using DomainObjects.Interfaces;
using DomainObjects.Interfaces.Handlers;
using DomainObjects.Models;

namespace BLL.Handlers
{
    public class UserHandler : IUserHandler
    {
        private readonly Dictionary<string, User> _users = new Dictionary<string, User>();
        public void Add(User user, string socketId)
        {
            _users.Add(socketId, user);
        }

        public User Remove(User user)
        {
            var storedUSer = _users.FirstOrDefault(x => x.Value == user);
            
            var key = storedUSer.Key;
            _users.Remove(key);
            return user;
        }

        public User Remove(string socketId)
        {
            var user = _users.GetValueOrDefault(socketId);
            this._users.Remove(socketId);
            return user;
        }

        public List<User> GetUsers()
        {
            return _users.Values.ToList();
        }
    }
}