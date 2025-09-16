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
    public class ProductUnitTest
    {
       

        [TestMethod]
        public void TestGetProductNotFound()
        {
            Connector c = GetConnection.GetConnector();
            ProductRepository productRepository = new ProductRepository(c);
            Product Product = productRepository.GetProduct(100);
            Assert.IsNull(Product);
        }

        

        [TestMethod]
        public void TestGetProductByNameNotFound()
        {
            Connector c = GetConnection.GetConnector();
            ProductRepository productRepository = new ProductRepository(c);
            Product Product = productRepository.GetProduct("Lacey");
            Assert.IsNull(Product);
        }

       

        

        
    }
}