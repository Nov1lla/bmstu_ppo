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
    public class CartUnitTest
    {
       
        [TestMethod]
        public void TestGetCartNotFound()
        {
            Connector c = GetConnection.GetConnector();
            CartRepository cartRepository = new CartRepository(c);
            Cart Cart = cartRepository.GetCart(100);
            Assert.IsNull(Cart);
        }

       

        [TestMethod]
        public void TestDelCart()
        {
            Connector c = GetConnection.GetConnector();
            CartRepository cartRepository = new CartRepository(c);
            Cart deletedCart = cartRepository.GetCart(9);
            if (deletedCart == null)
            {
                Assert.IsNull(deletedCart);
            }
            else
            {
                cartRepository.DelCart(deletedCart);
                Cart expectedCart = cartRepository.GetCart(9);
                Assert.IsNull(expectedCart);
            }
        }



    }
}