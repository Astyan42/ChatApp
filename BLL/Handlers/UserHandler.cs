using System.Collections.Generic;
using DomainObjects.Interfaces;
using DomainObjects.Interfaces.Handlers;
using DomainObjects.Models;

namespace BLL.Handlers
{
    public class UserHandler : IUserHandler
    {
        private readonly List<User> _users = new List<User>();
        public void Add(User user)
        {
            _users.Add(user);
        }

        public void Remove(User user)
        {
            _users.Remove(user);
        }

        public List<User> GetUsers()
        {
            return _users;
        }
    }
}