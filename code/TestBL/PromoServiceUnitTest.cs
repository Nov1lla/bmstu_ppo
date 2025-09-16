using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BL.Models;
using BL.RepositoryInterfaces;
using BL.Services;
using Moq;

namespace TestBL
{
    [TestClass]
    public class PromoServiceUnitTest
    {
        private readonly PromoService _promoService;
        private readonly Mock<IUserPromoRepository> _mockUserPromoRepository = new();
        private readonly Mock<IUserRepository> _mockUserRepository = new();
        private readonly Mock<IPromoRepository> _mockPromoRepository = new();

        public PromoServiceUnitTest()
        {
            _promoService = new PromoService(_mockPromoRepository.Object, _mockUserRepository.Object, _mockUserPromoRepository.Object);
        }
        [TestMethod]
        private void compare(Promo x, Promo y)
        {
            Assert.AreEqual(x.Code, y.Code);
            Assert.AreEqual(x.Discount, y.Discount);
            Assert.AreEqual(x.Data_start, y.Data_start);
            Assert.AreEqual(x.Data_end, y.Data_end);
        }

        [TestMethod]
        public void TestGetPromoDefault()
        {
            var users = CreateMockUser();
            var promos = CreateMockPromo();
            var userpromos = CreateMockUserPromo(users, promos);
            var promoId = promos[0].Id;

            _mockPromoRepository.Setup(s => s.GetPromo(promoId)).Returns(promos.Find(e => e.Id == promoId)!);


            Promo promoFound = _promoService.GetPromo(promoId);
            compare(promos[0], promoFound);
        }

        [TestMethod]
        public void TestGetPromoNotFound()
        {
            var users = CreateMockUser();
            var promos = CreateMockPromo();
            var userpromos = CreateMockUserPromo(users, promos);
            var promoId = Guid.NewGuid();

            _mockPromoRepository.Setup(s => s.GetPromo(promoId)).Returns(promos.Find(e => e.Id == promoId)!);


            Promo promoFound = _promoService.GetPromo(promoId);
            Assert.IsNull(promoFound);
        }

        public void TestGetPromoByIdUserDefault()
        {
            var users = CreateMockUser();
            var promos = CreateMockPromo();
            var userpromos = CreateMockUserPromo(users, promos);
            var userId = users[0].Id;
            Promo promo = CreatePromo(Guid.NewGuid());
            Guid promoId = Guid.NewGuid();

            _mockPromoRepository.Setup(s => s.GetPromo(promoId)).Returns(promos.Find(e => e.Id == promoId)!);

            _mockUserPromoRepository.Setup(s => s.GetUserPromoByIdUser(userId))
                        .Returns(userpromos.Where(e => e.Id_user == userId).ToList());

            _mockPromoRepository.Setup(s => s.IsExistPromo(promo)).Returns(promos.Contains(promo));
            _mockPromoRepository.Setup(s => s.AddPromo(It.IsAny<Promo>())).Callback((Promo promo) => promos.Add(promo)).Verifiable();

            List<Promo> promoFound = _promoService.GetPromoByIdUser(userId);
            Assert.AreEqual(promoFound.Count(), 1);
        }

        [TestMethod]
        public void TestGetPromoByIdUserNotFound()
        {
            var users = CreateMockUser();
            var promos = CreateMockPromo();
            var userpromos = CreateMockUserPromo(users, promos);
            var userId = Guid.NewGuid();
            Promo promo = CreatePromo(Guid.NewGuid());
            Guid promoId = Guid.NewGuid();

            _mockPromoRepository.Setup(s => s.IsExistPromo(promo)).Returns(promos.Contains(promo));
            _mockPromoRepository.Setup(s => s.GetPromo(promoId)).Returns(promos.Find(e => e.Id == promoId)!);

            _mockUserPromoRepository.Setup(s => s.GetUserPromoByIdUser(userId))
                        .Returns(userpromos.Where(e => e.Id_user == userId).ToList());

            _mockPromoRepository.Setup(s => s.AddPromo(It.IsAny<Promo>())).Callback((Promo promo) => promos.Add(promo)).Verifiable();

            List<Promo> promoFound = _promoService.GetPromoByIdUser(userId);
            Assert.AreEqual(promoFound.Count(), 0);
        }

        [TestMethod]
        public void TestAddPromoDefault()
        {
            var users = CreateMockUser();
            var promos = CreateMockPromo();
            var length = promos.Count();
            var userpromos = CreateMockUserPromo(users, promos);
            var promoId = Guid.NewGuid();
            var promo = CreatePromo(promoId);
            _mockPromoRepository.Setup(s => s.GetPromo(promoId)).Returns(promos.Find(e => e.Id == promoId)!);
            _mockPromoRepository.Setup(s => s.AddPromo(It.IsAny<Promo>())).Callback((Promo promo) => promos.Add(promo)).Verifiable();

            _promoService.AddPromo(promo);

            Assert.AreEqual(length + 1, promos.Count());
            compare(promo, promos.Find(e => e.Id == promoId)!);
        }

        [TestMethod]
        public void TestAddPromoIsExisted()
        {
            var users = CreateMockUser();
            var promos = CreateMockPromo();
            var length = promos.Count();
            var userpromos = CreateMockUserPromo(users, promos);
            var promo = promos[0];
            var promoId = promos[0].Id;
            _mockPromoRepository.Setup(s => s.IsExistPromo(promo)).Returns(promos.Contains(promo));
            _mockPromoRepository.Setup(s => s.GetPromo(promoId)).Returns(promos.Find(e => e.Id == promoId)!);
            _mockPromoRepository.Setup(s => s.AddPromo(It.IsAny<Promo>())).Callback((Promo promo) => promos.Add(promo)).Verifiable();

            Assert.ThrowsException<Exception>(() => _promoService.AddPromo(promo));
        }

        public void TestDelPromoDefault()
        {
            var users = CreateMockUser();
            var promos = CreateMockPromo();
            var length = promos.Count();
            var userpromos = CreateMockUserPromo(users, promos);
            var promo = promos[0];
            var promoId = promo.Id;
            _mockPromoRepository.Setup(s => s.IsExistPromo(promo)).Returns(promos.Contains(promo));
            _mockPromoRepository.Setup(s => s.GetPromo(promoId)).Returns(promos.Find(e => e.Id == promoId)!);
            _mockPromoRepository.Setup(s => s.DelPromo(It.IsAny<Promo>())).Callback((Promo promo) => promos.Remove(promo)).Verifiable();

            _promoService.DelPromo(promo);

            Assert.AreEqual(length - 1, promos.Count());
        }

        [TestMethod]
        public void TestDelPromoIsNotExisted()
        {
            var users = CreateMockUser();
            var promos = CreateMockPromo();
            var length = promos.Count();
            var userpromos = CreateMockUserPromo(users, promos);
            var promoId = Guid.NewGuid();
            var promo = CreatePromo(promoId);
            _mockPromoRepository.Setup(s => s.IsExistPromo(promo)).Returns(promos.Contains(promo));
            _mockPromoRepository.Setup(s => s.GetPromo(promoId)).Returns(promos.Find(e => e.Id == promoId)!);
            _mockPromoRepository.Setup(s => s.DelPromo(It.IsAny<Promo>())).Callback((Promo promo) => promos.Remove(promo)).Verifiable();

            Assert.ThrowsException<Exception>(() => _promoService.DelPromo(promo));
        }

        public void TestUpdatePromoDefault()
        {
            var users = CreateMockUser();
            var promos = CreateMockPromo();
            var length = promos.Count();
            var userpromos = CreateMockUserPromo(users, promos);
            var promo = promos[0];
            var promoId = promo.Id;
            promo.Discount = 1;
            _mockPromoRepository.Setup(s => s.GetPromo(promoId)).Returns(promos.Find(e => e.Id == promoId)!);
            _mockPromoRepository.Setup(s => s.UpdatePromo(It.IsAny<Promo>()))
                                .Callback((Promo promo) =>
                                {
                                    promos.Remove(item: promos.Find(e => e.Id == promo.Id)!);
                                    promos.Add(promo);
                                }).Verifiable();

            _mockPromoRepository.Setup(s => s.IsExistPromo(promo)).Returns(promos.Contains(promo));

            _promoService.UpdatePromo(promo);

            compare(promo, _promoService.GetPromo(promoId));
        }

        [TestMethod]
        public void TestUpdatePromoIsNotExisted()
        {
            var users = CreateMockUser();
            var promos = CreateMockPromo();
            var length = promos.Count();
            var userpromos = CreateMockUserPromo(users, promos);
            var promoId = Guid.NewGuid();
            var promo = CreatePromo(promoId);
            promo.Discount = 1;
            _mockPromoRepository.Setup(s => s.GetPromo(promoId)).Returns(promos.Find(e => e.Id == promoId)!);
            _mockPromoRepository.Setup(s => s.UpdatePromo(It.IsAny<Promo>()))
                                .Callback((Promo promo) =>
                                {
                                    promos.Remove(item: promos.Find(e => e.Id == promo.Id)!);
                                    promos.Add(promo);
                                }).Verifiable();

            _mockPromoRepository.Setup(s => s.IsExistPromo(promo)).Returns(promos.Contains(promo));

            Assert.ThrowsException<Exception>(() => _promoService.UpdatePromo(promo));
        }

        private List<User> CreateMockUser()
        {
            return new List<User>()
        {
            new User(Guid.NewGuid(), "test", "+7-926-719-75-12", "Moscow", "abc@gmail.com", "login", "123", "client"),
            new User(Guid.NewGuid(), "test1", "+7-926-719-75-12", "Moscow", "abc@gmail.com", "login1", "123", "admin"),
            new User(Guid.NewGuid(), "test2", "+7-926-719-75-12", "Moscow", "abc@gmail.com", "login2", "123", "client")};
        }

        private List<UserPromo> CreateMockUserPromo(List<User> users, List<Promo> promos)
        {
            return new List<UserPromo>()
            {
                new UserPromo(Guid.NewGuid(), users[0].Id, promos[0].Id),
                new UserPromo(Guid.NewGuid(), users[1].Id, promos[1].Id),
                new UserPromo(Guid.NewGuid(), users[2].Id, promos[2].Id)
            };
        }
        private Promo CreatePromo(Guid promoId)
        {
            return new Promo(promoId, "test3", 20, "20/12/2023", "20/12/2024");
        }
        private List<Promo> CreateMockPromo()
        {
            return new List<Promo>()
        {
            new Promo(Guid.NewGuid(), "test", 10, "20/10/2024", "20/10/2025"),
            new Promo(Guid.NewGuid(), "test1", 10, "20/10/2024", "20/10/2025"),
            new Promo(Guid.NewGuid(), "test2", 10, "20/10/2024", "20/10/2025")};
        }

    }
}