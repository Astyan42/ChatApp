using System.Collections.Generic;
using DomainObjects.Models;

namespace DomainObjects.Interfaces.Handlers
{
    public interface IUserHandler
    {
        void Add(User user, string socketId);
        User Remove(User user);
        User Remove(string socketId);
        List<User> GetUsers();
    }
}