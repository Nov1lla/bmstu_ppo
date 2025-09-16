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
    public class OrderUnitTest
    {
       

        [TestMethod]
        public void TestGetOrderNotFound()
        {
            Connector c = GetConnection.GetConnector();
            OrderRepository orderRepository = new OrderRepository(c);
            Order Order = orderRepository.GetOrder(100);
            Assert.IsNull(Order);
        }

       

        [TestMethod]
        public void TestDelOrder()
        {
            Connector c = GetConnection.GetConnector();
            OrderRepository orderRepository = new OrderRepository(c);
            Order deletedOrder = orderRepository.GetOrder(3);
            if (deletedOrder == null)
            {
                Assert.IsNull(deletedOrder);
            }
            else
            {
                orderRepository.DelOrder(deletedOrder);
                Order expectedOrder = orderRepository.GetOrder(3);
                Assert.IsNull(expectedOrder);
            }
        }

      

       

    }
}