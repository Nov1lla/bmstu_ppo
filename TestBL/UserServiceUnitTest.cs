using System;
using BL.Models;
using BL.RepositoryInterfaces;
using BL.Services;
using Moq;

namespace TestBL
{
    [TestClass]
    public class UserServiceUnitTest
    {
        private readonly UserService _userService;
        private readonly Mock<IUserRepository> _mockUserRepository = new();

        public UserServiceUnitTest()
        {
            _userService = new UserService(_mockUserRepository.Object);
        }
        [TestMethod]
        private void compare(User x, User y)
        {
            Assert.AreEqual(x.Login, y.Login);
            Assert.AreEqual(x.Password, y.Password);
            Assert.AreEqual(x.Name, y.Name);
            Assert.AreEqual(x.Phone, y.Phone);
            Assert.AreEqual(x.Address, y.Address);
            Assert.AreEqual(x.Role, y.Role);
            Assert.AreEqual(x.Email, y.Email);
        }

        [TestMethod]
        public void TestRegisterDefault()
        {
            var user = CreateUser(1);

            User createdUser = _userService.Register(user.Name, user.Phone, user.Address, user.Email, user.Login, user.Password, user.Role);

            compare(user, createdUser);
        }

        [TestMethod]
        public void TestRegisterIsExisted()
        {
            var users = CreateMockUsers();
            var user = CreateUser(users.Count + 1);
            var login = user.Login;
            users.Add(user);

            _mockUserRepository.Setup(s => s.GetUser(login)).Returns(users.Find(e => e.Login == login)!);

            Assert.ThrowsException<Exception>(() => _userService.Register(user.Name, user.Phone, user.Address, user.Email, user.Login, user.Password, user.Role));
        }

        [TestMethod]
        public void TestLogInDefault()
        {
            var users = CreateMockUsers();
            var user = CreateUser(users.Count + 1);
            var login = "login";
            users.Add(user);

            _mockUserRepository.Setup(s => s.GetUser(login)).Returns(users.Find(e => e.Login == login)!);

            User loggedUser = _userService.LogIn(login, "123");
            User expectedUser = users[0];
            compare(expectedUser, loggedUser);
        }

        [TestMethod]
        public void TestLogInWrongPassword()
        {
            var users = CreateMockUsers();
            var user = CreateUser(users.Count + 1);
            var login = "login";
            users.Add(user);

            _mockUserRepository.Setup(s => s.GetUser(login)).Returns(users.Find(e => e.Login == login)!);

            Assert.ThrowsException<Exception>(() => _userService.LogIn(login, "1234"));
        }

        [TestMethod]
        public void TestLogInNotExist()
        {
            var users = CreateMockUsers();
            var user = CreateUser(users.Count + 1);
            var login = "login4";
            users.Add(user);

            _mockUserRepository.Setup(s => s.GetUser(login)).Returns(users.Find(e => e.Login == login)!);

            Assert.ThrowsException<Exception>(() => _userService.LogIn(login, "1234"));
        }

        [TestMethod]
        public void TestChangePasswordDefault()
        {
            var users = CreateMockUsers();
            var user = CreateUser(users.Count + 1);
            var login = user.Login;
            User userUpdate = users[0];
            _mockUserRepository.Setup(s => s.GetUser(login)).Returns(users.Find(e => e.Login == login)!);
            _mockUserRepository.Setup(s => s.AddUser(It.IsAny<User>())).Callback((User user) => users.Add(user)).Verifiable();
            _mockUserRepository.Setup(s => s.DelUser(It.IsAny<User>())).Callback((User user) => users.Remove(user)).Verifiable();
            _mockUserRepository.Setup(s => s.UpdateUser(It.IsAny<User>())).Callback((User user) 
                            => {users.Remove(item: users.Find(e => e.Login == user.Login)!);
                                users.Add(user); }).Verifiable();

            _userService.ChangePassword(userUpdate.Login, "abc");
            Assert.AreEqual(users.Find(e => e.Login == userUpdate.Login)!.Password, "abc");
            Assert.AreEqual(users.Count, 3);

        }

        private User CreateUser(int userId)
        {
            return new User(userId, "test", "+7-926-719-75-12", "Moscow", "abc@gmail.com", "login", "123", "client");
        }

        private List<User> CreateMockUsers()
        {
            return new List<User>()
        {
            new User(1, "test", "+7-926-719-75-12", "Moscow", "abc@gmail.com", "login", "123", "client"),
            new User(2, "test1", "+7-926-719-75-12", "Moscow", "abc@gmail.com", "login1", "123", "admin"),
            new User(3, "test2", "+7-926-719-75-12", "Moscow", "abc@gmail.com", "login2", "123", "client")};
        }
    }
}