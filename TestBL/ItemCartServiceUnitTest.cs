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
    public class ItemCartServiceUnitTest
    {
        private readonly ItemCartService _itemCartService;
        private readonly Mock<IItemCartRepository> _mockItemCartRepository = new();

        public ItemCartServiceUnitTest()
        {
            _itemCartService = new ItemCartService(_mockItemCartRepository.Object);
        }
        [TestMethod]
        private void compare(ItemCart x, ItemCart y)
        {
            Assert.AreEqual(x.Id_cart, y.Id_cart);
            Assert.AreEqual(x.Id_product, y.Id_product);
            Assert.AreEqual(x.Quantity, y.Quantity);
        }

        [TestMethod]
        public void TestGetItemCartByIdDefault()
        {
            var users = CreateMockUsers();
            var products = CreateMockProduct();
            var carts = CreateMockCart(users);
            var itemCarts = CreateMockItemCart(carts, products);
            var itemCartId = itemCarts[0].Id;

            _mockItemCartRepository.Setup(s => s.GetItemCart(itemCartId)).Returns(itemCarts.Find(e => e.Id == itemCartId)!);


            ItemCart itemCartFound = _itemCartService.GetItemCartById(itemCartId);
            compare(itemCarts[0], itemCartFound);
        }

        [TestMethod]
        public void TestGetItemCartByIdNotFound()
        {
            var users = CreateMockUsers();
            var products = CreateMockProduct();
            var carts = CreateMockCart(users);
            var itemCarts = CreateMockItemCart(carts, products);
            var itemCartId = itemCarts.Count + 1;

            _mockItemCartRepository.Setup(s => s.GetItemCart(itemCartId)).Returns(itemCarts.Find(e => e.Id == itemCartId)!);


            ItemCart itemCartFound = _itemCartService.GetItemCartById(itemCartId);
            Assert.IsNull(itemCartFound);
        }

        [TestMethod]
        public void TestAddItemCartDefault()
        {
            var users = CreateMockUsers();
            var products = CreateMockProduct();
            var carts = CreateMockCart(users);
            var itemCarts = CreateMockItemCart(carts, products);
            int length = itemCarts.Count();
            var itemCart = CreateItemCart(itemCarts.Count + 1);

            _mockItemCartRepository.Setup(s => s.AddItemCart(It.IsAny<ItemCart>()))
                            .Callback((ItemCart itemCart) => itemCarts.Add(itemCart)).Verifiable();

            _mockItemCartRepository.Setup(s => s.IsExistItemCart(itemCart)).Returns(itemCarts.Contains(itemCart));

            _itemCartService.AddItemCart(itemCart);

            Assert.AreEqual(length + 1, itemCarts.Count());
        }

        [TestMethod]
        public void TestAddCartIsExisted()
        {
            var users = CreateMockUsers();
            var products = CreateMockProduct();
            var carts = CreateMockCart(users);
            var itemCarts = CreateMockItemCart(carts, products);
            int length = itemCarts.Count();
            var itemCart = itemCarts[0];

            _mockItemCartRepository.Setup(s => s.AddItemCart(It.IsAny<ItemCart>()))
                            .Callback((ItemCart itemCart) => itemCarts.Add(itemCart)).Verifiable();

            _mockItemCartRepository.Setup(s => s.IsExistItemCart(itemCart)).Returns(itemCarts.Contains(itemCart));

            Assert.ThrowsException<Exception>(() => _itemCartService.AddItemCart(itemCart));
        }

        public void TestDelItemCartDefault()
        {
            var users = CreateMockUsers();
            var products = CreateMockProduct();
            var carts = CreateMockCart(users);
            var itemCarts = CreateMockItemCart(carts, products);
            int length = itemCarts.Count();
            var itemCart = itemCarts[0];

            _mockItemCartRepository.Setup(s => s.DelItemCart(It.IsAny<ItemCart>()))
                            .Callback((ItemCart itemCart) => itemCarts.Remove(itemCart)).Verifiable();

            _mockItemCartRepository.Setup(s => s.IsExistItemCart(itemCart)).Returns(itemCarts.Contains(itemCart));

            _itemCartService.DelItemCart(itemCart);

            Assert.AreEqual(length - 1, itemCarts.Count());
        }

        [TestMethod]
        public void TestDelItemCartIsNotExisted()
        {
            var users = CreateMockUsers();
            var products = CreateMockProduct();
            var carts = CreateMockCart(users);
            var itemCarts = CreateMockItemCart(carts, products);
            int length = itemCarts.Count();
            var itemCart = CreateItemCart(itemCarts.Count + 1);

            _mockItemCartRepository.Setup(s => s.DelItemCart(It.IsAny<ItemCart>()))
                            .Callback((ItemCart itemCart) => itemCarts.Remove(itemCart)).Verifiable();

            _mockItemCartRepository.Setup(s => s.IsExistItemCart(itemCart)).Returns(itemCarts.Contains(itemCart));

            Assert.ThrowsException<Exception>(() => _itemCartService.DelItemCart(itemCart));
        }

        public void TestUpdateItemCartDefault()
        {
            var users = CreateMockUsers();
            var products = CreateMockProduct();
            var carts = CreateMockCart(users);
            var itemCarts = CreateMockItemCart(carts, products);
            int length = itemCarts.Count();
            var itemCart = itemCarts[0];

            itemCart.Quantity = 0;

            _mockItemCartRepository.Setup(s => s.AddItemCart(It.IsAny<ItemCart>()))
                            .Callback((ItemCart itemCart) => itemCarts.Add(itemCart)).Verifiable();

            _mockItemCartRepository.Setup(s => s.DelItemCart(It.IsAny<ItemCart>()))
                            .Callback((ItemCart itemCart) => itemCarts.Remove(itemCart)).Verifiable();

            _mockItemCartRepository.Setup(s => s.IsExistItemCart(itemCart)).Returns(itemCarts.Contains(itemCart));

            _mockItemCartRepository.Setup(s => s.UpdateItemCart(It.IsAny<ItemCart>()))
                                .Callback((ItemCart itemCart) =>
                                {
                                    itemCarts.Remove(item: itemCarts.Find(e => e.Id == itemCart.Id)!);
                                    itemCarts.Add(itemCart);
                                }).Verifiable();

            _itemCartService.UpdateItemCart(itemCart);

            compare(itemCart, _itemCartService.GetItemCartById(itemCart.Id));
        }

        [TestMethod]
        public void TestUpdateCartIsNotExisted()
        {
            var users = CreateMockUsers();
            var products = CreateMockProduct();
            var carts = CreateMockCart(users);
            var itemCarts = CreateMockItemCart(carts, products);
            int length = itemCarts.Count();
            var itemCart = CreateItemCart(itemCarts.Count + 1);

            itemCart.Quantity = 0;

            _mockItemCartRepository.Setup(s => s.AddItemCart(It.IsAny<ItemCart>()))
                            .Callback((ItemCart itemCart) => itemCarts.Add(itemCart)).Verifiable();

            _mockItemCartRepository.Setup(s => s.DelItemCart(It.IsAny<ItemCart>()))
                            .Callback((ItemCart itemCart) => itemCarts.Remove(itemCart)).Verifiable();

            _mockItemCartRepository.Setup(s => s.IsExistItemCart(itemCart)).Returns(itemCarts.Contains(itemCart));

            _mockItemCartRepository.Setup(s => s.UpdateItemCart(It.IsAny<ItemCart>()))
                                .Callback((ItemCart itemCart) =>
                                {
                                    itemCarts.Remove(item: itemCarts.Find(e => e.Id == itemCart.Id)!);
                                    itemCarts.Add(itemCart);
                                }).Verifiable();

            Assert.ThrowsException<Exception>(() => _itemCartService.UpdateItemCart(itemCart));
        }

        private List<User> CreateMockUsers()
        {
            return new List<User>()
        {
            new User(1, "test", "+7-926-719-75-12", "Moscow", "abc@gmail.com", "login", "123", "client"),
            new User(2, "test1", "+7-926-719-75-12", "Moscow", "abc@gmail.com", "login1", "123", "admin"),
            new User(3, "test2", "+7-926-719-75-12", "Moscow", "abc@gmail.com", "login2", "123", "client")};
        }

        private List<Cart> CreateMockCart(List<User> users)
        {
            return new List<Cart>()
        {
            new Cart(1,"21/11/2023",  users[0].Id),
            new Cart(2, "11/10/2021", users[1].Id),
            new Cart(3, "23/12/2022", users[2].Id)};
        }

        private List<Product> CreateMockProduct()
        {
            return new List<Product>()
        {
            new Product(1, "Legion", 1200, 3, "Lenovo", "made in China"),
            new Product(2, "Macbook", 1500, 2, "Apple", "USA"),
            new Product(3, "Tab", 1200, 3, "SamSumg", "Hanoi")};
        }

        private ItemCart CreateItemCart(int itemCartId)
        {
            return new ItemCart(itemCartId, 1, 2, 10);
        }

        private List<ItemCart> CreateMockItemCart(List<Cart> carts, List<Product> products)
        {
            return new List<ItemCart>()
        {
            new ItemCart(1, products[0].Id, carts[0].Id, 10),
            new ItemCart(2, products[1].Id, carts[1].Id, 11),
            new ItemCart(3, products[2].Id, carts[2].Id, 12)};
        }
    }
}