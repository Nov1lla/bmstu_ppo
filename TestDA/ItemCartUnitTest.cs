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
    public class ItemCartUnitTest
    {
      

        [TestMethod]
        public void TestGetItemCartNotFound()
        {
            Connector c = GetConnection.GetConnector();
            ItemCartRepository cartRepository = new ItemCartRepository(c);
            ItemCart ItemCart = cartRepository.GetItemCart(100);
            Assert.IsNull(ItemCart);
        }


        [TestMethod]
        public void TestDelCart()
        {
            Connector c = GetConnection.GetConnector();
            ItemCartRepository cartRepository = new ItemCartRepository(c);
            ItemCart deletedItemCart = cartRepository.GetItemCart(9);
            if (deletedItemCart == null)
            {
                Assert.IsNull(deletedItemCart);
            }
            else
            {
                cartRepository.DelItemCart(deletedItemCart);
                ItemCart expectedItemCart = cartRepository.GetItemCart(9);
                Assert.IsNull(expectedItemCart);
            }
        }

        

        [TestMethod]
        public void TestIsExistCart()
        {
            Connector c = GetConnection.GetConnector();
            ItemCartRepository cartRepository = new ItemCartRepository(c);
            ItemCart ItemCart = cartRepository.GetItemCart(1);
            bool flag = cartRepository.IsExistItemCart(ItemCart);
            Assert.IsTrue(flag);
        }

    }
}