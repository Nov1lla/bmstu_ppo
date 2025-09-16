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
    public class ItemOrderUnitTest
    {
       

        [TestMethod]
        public void TestGetItemOrderNotFound()
        {
            Connector c = GetConnection.GetConnector();
            ItemOrderRepository orderRepository = new ItemOrderRepository(c);
            ItemOrder ItemOrder = orderRepository.GetItemOrder(100);
            Assert.IsNull(ItemOrder);
        }

        

        [TestMethod]
        public void TestDelOrder()
        {
            Connector c = GetConnection.GetConnector();
            ItemOrderRepository orderRepository = new ItemOrderRepository(c);
            ItemOrder deletedItemOrder = orderRepository.GetItemOrder(9);
            if (deletedItemOrder == null)
            {
                Assert.IsNull(deletedItemOrder);
            }
            else
            {
                orderRepository.DelItemOrder(deletedItemOrder);
                ItemOrder expectedItemOrder = orderRepository.GetItemOrder(3);
                Assert.IsNull(expectedItemOrder);
            }
        }

        

        [TestMethod]
        public void TestIsExistOrder()
        {
            Connector c = GetConnection.GetConnector();
            ItemOrderRepository orderRepository = new ItemOrderRepository(c);
            ItemOrder ItemOrder = orderRepository.GetItemOrder(1);
            bool flag = orderRepository.IsExistItemOrder(ItemOrder);
            Assert.IsTrue(flag);
        }

    }
}