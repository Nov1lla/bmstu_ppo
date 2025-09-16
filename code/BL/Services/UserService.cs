using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BL.Models;
using BL.RepositoryInterfaces;

namespace BL.Services
{
    public class UserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository ?? throw new Exception("Параметр пустой!");
        }

        public User Register(string name, string phone, string address, string email, string login, string password, string role)
        {
            User user = _userRepository.GetUser(login);
            if (user == null)
            {
                int countUsers = _userRepository.CountAllUsers();
                user = new User(Guid.NewGuid(), name, phone, address, email, login, password, role);
                _userRepository.AddUser(user);
            }
            else
            {
                throw new Exception("Пользователь с таким логином уже существует.");
            }
            return user;
        }

        public User LogIn (string login, string password)
        {
            User user = _userRepository.GetUser(login);
            if (user != null) 
            { 
                if (user.Password != password) 
                {
                    throw new Exception("Неверный пароль.");
                }
            }
            else
            {
                throw new Exception("Не существует пользователя с таким логином.");
            }

            return user;
        }

        public User ChangePassword(string login, string newPassword)
        {
            User user = _userRepository.GetUser(login);
            user.Password = newPassword;
            _userRepository.UpdateUser(user);
            return user;
        }

        public void DeleteUser(User user)
        {
            _userRepository.DelUser(user);
        }
    }
}
