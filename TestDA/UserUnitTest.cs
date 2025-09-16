using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BL.RepositoryInterfaces;
using BL.Models;
using BL.MyException;
using Microsoft.Extensions.Logging.Abstractions;
using DA;
using lab04_Test.DA;

namespace TestDA
{
    [TestClass]
    public class UserUnitTest
    {
        

        [TestMethod]
        public void TestGetUserNotFound()
        {
            Connector c = GetConnection.GetConnector();
            UserRepository userRepository = new UserRepository(c);
            User User = userRepository.GetUser(100);
            Assert.IsNull(User);
        }

        

        [TestMethod]
        public void TestGetUserByLoginNotFound()
        {
            Connector c = GetConnection.GetConnector();
            UserRepository userRepository = new UserRepository(c);
            User User = userRepository.GetUser("Lacey");
            Assert.IsNull(User);
        }

        

        [TestMethod]
        public void TestDelUser()
        {
            Connector c = GetConnection.GetConnector();
            UserRepository userRepository = new UserRepository(c);
            User deletedUser = userRepository.GetUser(9);
            if (deletedUser == null)
            {
                Assert.IsNull(deletedUser);
            }
            else
            {
                userRepository.DelUser(deletedUser);
                User expectedUser = userRepository.GetUser(9);
                Assert.IsNull(expectedUser);
            }           
        }

        

        [TestMethod]
        public void TestGetAll()
        {
            Connector c = GetConnection.GetConnector();
            UserRepository userRepository = new UserRepository(c);
            List<User> users = userRepository.GetAll();
            int count = userRepository.CountAllUsers();
            Assert.IsNotNull(users);
            Assert.AreEqual(users.Count, count);
        }
    }
}