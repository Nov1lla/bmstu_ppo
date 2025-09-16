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
    public class ItemOrderServiceUnitTest
    {
        private readonly ItemOrderService _itemOrderService;
        private readonly Mock<IItemOrderRepository> _mockItemOrderRepository = new();

        public ItemOrderServiceUnitTest()
        {
            _itemOrderService = new ItemOrderService(_mockItemOrderRepository.Object);
        }
        [TestMethod]
        private void compare(ItemOrder x, ItemOrder y)
        {
            Assert.AreEqual(x.Id_product, y.Id_product);
            Assert.AreEqual(x.Id_order, y.Id_order);
            Assert.AreEqual(x.Quantity, y.Quantity);
        }

        [TestMethod]
        public void TestGetItemOrderByIdDefault()
        {
            var users = CreateMockUsers();
            var products = CreateMockProduct();
            var promos = CreateMockPromo();
            var orders = CreateMockOrder(users, promos);
            var itemOrders = CreateMockItemOrder(orders, products);
            var itemOrderId = itemOrders[0].Id;

            _mockItemOrderRepository.Setup(s => s.GetItemOrder(itemOrderId)).Returns(itemOrders.Find(e => e.Id == itemOrderId)!);


            ItemOrder itemOrderFound = _itemOrderService.GetItemOrderById(itemOrderId);
            compare(itemOrders[0], itemOrderFound);
        }

        [TestMethod]
        public void TestGetItemOrderByIdNotFound()
        {
            var users = CreateMockUsers();
            var products = CreateMockProduct();
            var promos = CreateMockPromo();
            var orders = CreateMockOrder(users, promos);
            var itemOrders = CreateMockItemOrder(orders, products);
            var itemOrderId = Guid.NewGuid();

            _mockItemOrderRepository.Setup(s => s.GetItemOrder(itemOrderId)).Returns(itemOrders.Find(e => e.Id == itemOrderId)!);


            ItemOrder itemOrderFound = _itemOrderService.GetItemOrderById(itemOrderId);
            Assert.IsNull(itemOrderFound);
        }

        [TestMethod]
        public void TestAddItemOrderDefault()
        {
            var users = CreateMockUsers();
            var products = CreateMockProduct();
            var promos = CreateMockPromo();
            var orders = CreateMockOrder(users, promos);
            var itemOrders = CreateMockItemOrder(orders, products);
            int length = itemOrders.Count();
            var itemOrder = CreateItemOrder(Guid.NewGuid());

            _mockItemOrderRepository.Setup(s => s.AddItemOrder(It.IsAny<ItemOrder>()))
                            .Callback((ItemOrder itemOrder) => itemOrders.Add(itemOrder)).Verifiable();

            _mockItemOrderRepository.Setup(s => s.IsExistItemOrder(itemOrder)).Returns(itemOrders.Contains(itemOrder));

            _itemOrderService.AddItemOrder(itemOrder);

            Assert.AreEqual(length + 1, itemOrders.Count());
        }

        [TestMethod]
        public void TestAddOrderIsExisted()
        {
            var users = CreateMockUsers();
            var products = CreateMockProduct();
            var promos = CreateMockPromo();
            var orders = CreateMockOrder(users, promos);
            var itemOrders = CreateMockItemOrder(orders, products);
            int length = itemOrders.Count();
            var itemOrder = itemOrders[0];

            _mockItemOrderRepository.Setup(s => s.AddItemOrder(It.IsAny<ItemOrder>()))
                            .Callback((ItemOrder itemOrder) => itemOrders.Add(itemOrder)).Verifiable();

            _mockItemOrderRepository.Setup(s => s.IsExistItemOrder(itemOrder)).Returns(itemOrders.Contains(itemOrder));

            Assert.ThrowsException<Exception>(() => _itemOrderService.AddItemOrder(itemOrder));
        }

        public void TestDelItemOrderDefault()
        {
            var users = CreateMockUsers();
            var products = CreateMockProduct();
            var promos = CreateMockPromo();
            var orders = CreateMockOrder(users, promos);
            var itemOrders = CreateMockItemOrder(orders, products);
            int length = itemOrders.Count();
            var itemOrder = itemOrders[0];

            _mockItemOrderRepository.Setup(s => s.DelItemOrder(It.IsAny<ItemOrder>()))
                            .Callback((ItemOrder itemOrder) => itemOrders.Remove(itemOrder)).Verifiable();

            _mockItemOrderRepository.Setup(s => s.IsExistItemOrder(itemOrder)).Returns(itemOrders.Contains(itemOrder));

            _itemOrderService.DelItemOrder(itemOrder);

            Assert.AreEqual(length - 1, itemOrders.Count());
        }

        [TestMethod]
        public void TestDelItemOrderIsNotExisted()
        {
            var users = CreateMockUsers();
            var products = CreateMockProduct();
            var promos = CreateMockPromo();
            var orders = CreateMockOrder(users, promos);
            var itemOrders = CreateMockItemOrder(orders, products);
            int length = itemOrders.Count();
            var itemOrder = CreateItemOrder(Guid.NewGuid());

            _mockItemOrderRepository.Setup(s => s.DelItemOrder(It.IsAny<ItemOrder>()))
                            .Callback((ItemOrder itemOrder) => itemOrders.Remove(itemOrder)).Verifiable();

            _mockItemOrderRepository.Setup(s => s.IsExistItemOrder(itemOrder)).Returns(itemOrders.Contains(itemOrder));

            Assert.ThrowsException<Exception>(() => _itemOrderService.DelItemOrder(itemOrder));
        }

        public void TestUpdateItemOrderDefault()
        {
            var users = CreateMockUsers();
            var products = CreateMockProduct();
            var promos = CreateMockPromo();
            var orders = CreateMockOrder(users, promos);
            var itemOrders = CreateMockItemOrder(orders, products);
            int length = itemOrders.Count();
            var itemOrder = itemOrders[0];

            itemOrder.Quantity = 0;

            _mockItemOrderRepository.Setup(s => s.AddItemOrder(It.IsAny<ItemOrder>()))
                            .Callback((ItemOrder itemOrder) => itemOrders.Add(itemOrder)).Verifiable();

            _mockItemOrderRepository.Setup(s => s.DelItemOrder(It.IsAny<ItemOrder>()))
                            .Callback((ItemOrder itemOrder) => itemOrders.Remove(itemOrder)).Verifiable();

            _mockItemOrderRepository.Setup(s => s.IsExistItemOrder(itemOrder)).Returns(itemOrders.Contains(itemOrder));

            _mockItemOrderRepository.Setup(s => s.UpdateItemOrder(It.IsAny<ItemOrder>()))
                                .Callback((ItemOrder itemOrder) =>
                                {
                                    itemOrders.Remove(item: itemOrders.Find(e => e.Id == itemOrder.Id)!);
                                    itemOrders.Add(itemOrder);
                                }).Verifiable();

            _itemOrderService.UpdateItemOrder(itemOrder);

            compare(itemOrder, _itemOrderService.GetItemOrderById(itemOrder.Id));
        }

        [TestMethod]
        public void TestUpdateOrderIsNotExisted()
        {
            var users = CreateMockUsers();
            var products = CreateMockProduct();
            var promos = CreateMockPromo();
            var orders = CreateMockOrder(users, promos);
            var itemOrders = CreateMockItemOrder(orders, products);
            int length = itemOrders.Count();
            var itemOrder = CreateItemOrder(Guid.NewGuid());

            itemOrder.Quantity = 0;

            _mockItemOrderRepository.Setup(s => s.AddItemOrder(It.IsAny<ItemOrder>()))
                            .Callback((ItemOrder itemOrder) => itemOrders.Add(itemOrder)).Verifiable();

            _mockItemOrderRepository.Setup(s => s.DelItemOrder(It.IsAny<ItemOrder>()))
                            .Callback((ItemOrder itemOrder) => itemOrders.Remove(itemOrder)).Verifiable();

            _mockItemOrderRepository.Setup(s => s.IsExistItemOrder(itemOrder)).Returns(itemOrders.Contains(itemOrder));

            _mockItemOrderRepository.Setup(s => s.UpdateItemOrder(It.IsAny<ItemOrder>()))
                                .Callback((ItemOrder itemOrder) =>
                                {
                                    itemOrders.Remove(item: itemOrders.Find(e => e.Id == itemOrder.Id)!);
                                    itemOrders.Add(itemOrder);
                                }).Verifiable();

            Assert.ThrowsException<Exception>(() => _itemOrderService.UpdateItemOrder(itemOrder));
        }

        private List<User> CreateMockUsers()
        {
            return new List<User>()
        {
            new User(Guid.NewGuid(), "test", "+7-926-719-75-12", "Moscow", "abc@gmail.com", "login", "123", "client"),
            new User(Guid.NewGuid(), "test1", "+7-926-719-75-12", "Moscow", "abc@gmail.com", "login1", "123", "admin"),
            new User(Guid.NewGuid(), "test2", "+7-926-719-75-12", "Moscow", "abc@gmail.com", "login2", "123", "client")};
        }

        private List<Promo> CreateMockPromo()
        {
            return new List<Promo>()
        {
            new Promo(Guid.NewGuid(), "test", 10, "20/10/2024", "20/10/2025"),
            new Promo(Guid.NewGuid(), "test1", 10, "20/10/2024", "20/10/2025"),
            new Promo(Guid.NewGuid(), "test2", 10, "20/10/2024", "20/10/2025")};
        }

        private List<Order> CreateMockOrder(List<User> users, List<Promo> promos)
        {
            return new List<Order>()
        {
            new Order(Guid.NewGuid(), "Shipped", "20/10/2024", users[0].Id, promos[0].Id),
            new Order(Guid.NewGuid(), "Shipped", "20/10/2024", users[1].Id, promos[1].Id),
            new Order(Guid.NewGuid(), "Shipped", "20/10/2024", users[2].Id, promos[2].Id)};
        }
        private List<Product> CreateMockProduct()
        {
            return new List<Product>()
        {
            new Product(Guid.NewGuid(), "Legion", 1200, 3, "Lenovo", "made in China"),
            new Product(Guid.NewGuid(), "Macbook", 1500, 2, "Apple", "USA"),
            new Product(Guid.NewGuid(), "Tab", 1200, 3, "SamSumg", "Hanoi")};
        }

        private ItemOrder CreateItemOrder(Guid itemOrderId)
        {
            return new ItemOrder(itemOrderId, Guid.NewGuid(), Guid.NewGuid(), 10);
        }

        private List<ItemOrder> CreateMockItemOrder(List<Order> orders, List<Product> products)
        {
            return new List<ItemOrder>()
        {
            new ItemOrder(Guid.NewGuid(), products[0].Id, orders[0].Id, 10),
            new ItemOrder(Guid.NewGuid(), products[1].Id, orders[1].Id, 11),
            new ItemOrder(Guid.NewGuid(), products[2].Id, orders[2].Id, 12)};
        }
    }
}