using System.Collections.Generic;
using DomainObjects.Models;

namespace DomainObjects.Interfaces.Handlers
{
    public interface IUserHandler
    {
        void Add(User user);
        void Remove(User user);
        List<User> GetUsers();
    }
}