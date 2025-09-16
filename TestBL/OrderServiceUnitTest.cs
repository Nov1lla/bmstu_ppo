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
    public class OrderServiceUnitTest
    {
        private readonly OrderService _orderService;
        private readonly Mock<IOrderRepository> _mockOrderRepository = new();

        public OrderServiceUnitTest()
        {
            _orderService = new OrderService(_mockOrderRepository.Object);
        }
        [TestMethod]
        private void compare(Order x, Order y)
        {
            Assert.AreEqual(x.Status, y.Status);
            Assert.AreEqual(x.Data_created, y.Data_created);
            Assert.AreEqual(x.Id_user, y.Id_user);
            Assert.AreEqual(x.Id_promo, y.Id_promo);
        }

        [TestMethod]
        public void TestGetOrderByIdDefault()
        {
            var users = CreateMockUser();
            var promos = CreateMockPromo();
            var orders = CreateMockOrder(users, promos);
            var orderId = orders[0].Id;

            _mockOrderRepository.Setup(s => s.GetOrder(orderId)).Returns(orders.Find(e => e.Id == orderId)!);


            Order orderFound = _orderService.GetOrderById(orderId);
            compare(orders[0], orderFound);
        }

        [TestMethod]
        public void TestGetOrderByIdNotFound()
        {
            var users = CreateMockUser();
            var promos = CreateMockPromo();
            var orders = CreateMockOrder(users, promos);
            var orderId = orders.Count + 1;

            _mockOrderRepository.Setup(s => s.GetOrder(orderId)).Returns(orders.Find(e => e.Id == orderId)!);


            Order OrderFound = _orderService.GetOrderById(orderId);
            Assert.IsNull(OrderFound);
        }

        [TestMethod]
        public void TestAddOrderDefault()
        {
            var users = CreateMockUser();
            var promos = CreateMockPromo();
            var orders = CreateMockOrder(users, promos);
            var length = orders.Count();
            var orderId = orders.Count + 1;
            var order = CreateOrder(orderId);

            _mockOrderRepository.Setup(s => s.AddOrder(It.IsAny<Order>())).Callback((Order order) => orders.Add(order)).Verifiable();

            _mockOrderRepository.Setup(s => s.IsExistOrder(order)).Returns(orders.Contains(order));

            _orderService.AddOrder(order);

            Assert.AreEqual(length + 1, orders.Count());
            compare(order, orders.Find(e => e.Id == orderId)!);
        }

        [TestMethod]
        public void TestAddOrderIsExisted()
        {
            var users = CreateMockUser();
            var promos = CreateMockPromo();
            var orders = CreateMockOrder(users, promos);
            var length = orders.Count();
            var orderId = orders.Count + 1;
            var order = orders[0];

            _mockOrderRepository.Setup(s => s.AddOrder(It.IsAny<Order>())).Callback((Order order) => orders.Add(order)).Verifiable();

            _mockOrderRepository.Setup(s => s.IsExistOrder(order)).Returns(orders.Contains(order));

            Assert.ThrowsException<Exception>(() => _orderService.AddOrder(order));
        }

        public void TestDelOrderDefault()
        {
            var users = CreateMockUser();
            var promos = CreateMockPromo();
            var orders = CreateMockOrder(users, promos);
            var length = orders.Count();
            var orderId = orders.Count + 1;
            var order = orders[0];

            _mockOrderRepository.Setup(s => s.DelOrder(It.IsAny<Order>())).Callback((Order order) => orders.Remove(order)).Verifiable();

            _mockOrderRepository.Setup(s => s.IsExistOrder(order)).Returns(orders.Contains(order));

            _orderService.DelOrder(order);

            Assert.AreEqual(length - 1, orders.Count());
        }

        [TestMethod]
        public void TestDelOrderIsNotExisted()
        {
            var users = CreateMockUser();
            var promos = CreateMockPromo();
            var orders = CreateMockOrder(users, promos);
            var length = orders.Count();
            var orderId = orders.Count + 1;
            var order = CreateOrder(orders.Count + 1);

            _mockOrderRepository.Setup(s => s.DelOrder(It.IsAny<Order>())).Callback((Order order) => orders.Remove(order)).Verifiable();

            _mockOrderRepository.Setup(s => s.IsExistOrder(order)).Returns(orders.Contains(order));

            Assert.ThrowsException<Exception>(() => _orderService.DelOrder(order));
        }

        public void TestUpdateOrderDefault()
        {
            var users = CreateMockUser();
            var promos = CreateMockPromo();
            var orders = CreateMockOrder(users, promos);
            var length = orders.Count();
            var orderId = orders.Count + 1;
            var order = orders[0];

            order.Status = "Done";
            _mockOrderRepository.Setup(s => s.UpdateOrder(It.IsAny<Order>()))
                                .Callback((Order order) =>
                                {
                                    orders.Remove(item: orders.Find(e => e.Id == order.Id)!);
                                    orders.Add(order);
                                }).Verifiable();

            _mockOrderRepository.Setup(s => s.IsExistOrder(order)).Returns(orders.Contains(order));

            _orderService.UpdateOrder(order);

            compare(order, _orderService.GetOrderById(orderId));
        }

        [TestMethod]
        public void TestUpdateOrderIsNotExisted()
        {
            var users = CreateMockUser();
            var promos = CreateMockPromo();
            var orders = CreateMockOrder(users, promos);
            var length = orders.Count();
            var orderId = orders.Count + 1;
            var order = CreateOrder(orderId);

            order.Status = "Done";
            _mockOrderRepository.Setup(s => s.UpdateOrder(It.IsAny<Order>()))
                                .Callback((Order order) =>
                                {
                                    orders.Remove(item: orders.Find(e => e.Id == order.Id)!);
                                    orders.Add(order);
                                }).Verifiable();

            _mockOrderRepository.Setup(s => s.IsExistOrder(order)).Returns(orders.Contains(order));

            Assert.ThrowsException<Exception>(() => _orderService.UpdateOrder(order));
        }

        private List<User> CreateMockUser()
        {
            return new List<User>()
        {
            new User(1, "test", "+7-926-719-75-12", "Moscow", "abc@gmail.com", "login", "123", "client"),
            new User(2, "test1", "+7-926-719-75-12", "Moscow", "abc@gmail.com", "login1", "123", "admin"),
            new User(3, "test2", "+7-926-719-75-12", "Moscow", "abc@gmail.com", "login2", "123", "client")};
        }

        private List<Promo> CreateMockPromo()
        {
            return new List<Promo>()
        {
            new Promo(1, "test", 10, "20/10/2024", "20/10/2025"),
            new Promo(2, "test1", 10, "20/10/2024", "20/10/2025"),
            new Promo(3, "test2", 10, "20/10/2024", "20/10/2025")};
        }

        private Order CreateOrder(int orderId)
        {
            return new Order(orderId, "Shipped", "20/10/2024", 1, 2);
        }

        private List<Order> CreateMockOrder(List<User> users, List<Promo> promos)
        {
            return new List<Order>()
        {
            new Order(1, "Shipped", "20/10/2024", users[0].Id, promos[0].Id),
            new Order(2, "Shipped", "20/10/2024", users[1].Id, promos[1].Id),
            new Order(3, "Shipped", "20/10/2024", users[2].Id, promos[2].Id)};
        }
    }
}