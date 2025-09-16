using BL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.RepositoryInterfaces
{
    public interface IUserRepository
    {
        User GetUser(int id);

        User GetUser(string login);

        void AddUser(User user);

        void DelUser(User user);

        void UpdateUser(User user);

        List<User> GetAll();

        int CountAllUsers();
    }
}
