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
    public class PromoUnitTest
    {
      
        

        [TestMethod]
        public void TestGetPromoNotFound()
        {
            Connector c = GetConnection.GetConnector();
            PromoRepository promoRepository = new PromoRepository(c);
            Promo Promo = promoRepository.GetPromo(100);
            Assert.IsNull(Promo);
        }

        

        [TestMethod]
        public void TestDelPromo()
        {
            Connector c = GetConnection.GetConnector();
            PromoRepository promoRepository = new PromoRepository(c);
            Promo deletedPromo = promoRepository.GetPromo(11);
            if (deletedPromo == null)
            {
                Assert.IsNull(deletedPromo);
            }
            else
            {
                promoRepository.DelPromo(deletedPromo);
                Promo expectedPromo = promoRepository.GetPromo(11);
                Assert.IsNull(expectedPromo);
            }
        }

        [TestMethod]
        public void TestUpdatePromo()
        {
            Connector c = GetConnection.GetConnector();
            PromoRepository promoRepository = new PromoRepository(c);
            Promo updatePromo = promoRepository.GetPromo(2);
            updatePromo.Discount = 24;
            promoRepository.UpdatePromo(updatePromo);
            Promo expectedPromo = promoRepository.GetPromo(2);
            Assert.IsNotNull(expectedPromo);
            Assert.AreEqual(expectedPromo.Discount, updatePromo.Discount);
        }
    }
}