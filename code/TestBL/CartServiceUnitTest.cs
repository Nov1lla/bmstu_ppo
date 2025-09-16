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
    public class CartServiceUnitTest
    {
        private readonly CartService _cartService;
        private readonly Mock<ICartRepository> _mockCartRepository = new();

        public CartServiceUnitTest()
        {
            _cartService = new CartService(_mockCartRepository.Object);
        }
        [TestMethod]
        private void compare(Cart x, Cart y)
        {
            Assert.AreEqual(x.Data_create, y.Data_create);
            Assert.AreEqual(x.Id_user, y.Id_user);
        }

        [TestMethod]
        public void TestGetCartByIdDefault()
        {
            var Carts = CreateMockCart();
            var CartId = Carts[0].Id;

            _mockCartRepository.Setup(s => s.GetCart(CartId)).Returns(Carts.Find(e => e.Id == CartId)!);


            Cart CartFound = _cartService.GetCartById(CartId);
            compare(Carts[0], CartFound);
        }

        [TestMethod]
        public void TestGetCartByIdNotFound()
        {
            var Carts = CreateMockCart();
            var CartId = Guid.NewGuid();

            _mockCartRepository.Setup(s => s.GetCart(CartId)).Returns(Carts.Find(e => e.Id == CartId)!);


            Cart CartFound = _cartService.GetCartById(CartId);
            Assert.IsNull(CartFound);
        }

        [TestMethod]
        public void TestAddCartDefault()
        {
            var carts = CreateMockCart();
            var length = carts.Count();
            var cartId = Guid.NewGuid();
            var cart = CreateCart(cartId);

            _mockCartRepository.Setup(s => s.AddCart(It.IsAny<Cart>())).Callback((Cart cart) => carts.Add(cart)).Verifiable();

            _mockCartRepository.Setup(s => s.IsExistCart(cart)).Returns(carts.Contains(cart));

            _cartService.AddCart(cart);

            Assert.AreEqual(length + 1, carts.Count());
        }

        [TestMethod]
        public void TestAddCartIsExisted()
        {
            var carts = CreateMockCart();
            var cart = carts[0];

            _mockCartRepository.Setup(s => s.AddCart(It.IsAny<Cart>())).Callback((Cart cart) => carts.Add(cart)).Verifiable();

            _mockCartRepository.Setup(s => s.IsExistCart(cart)).Returns(carts.Contains(cart));

            Assert.ThrowsException<Exception>(() => _cartService.AddCart(cart));
        }

        public void TestDelCartDefault()
        {
            var carts = CreateMockCart();
            var length = carts.Count();
            var cart = carts[0];

            _mockCartRepository.Setup(s => s.DelCart(It.IsAny<Cart>())).Callback((Cart cart) => carts.Remove(cart)).Verifiable();

            _mockCartRepository.Setup(s => s.IsExistCart(cart)).Returns(carts.Contains(cart));

            _cartService.DelCart(cart);

            Assert.AreEqual(length - 1, carts.Count());
        }

        [TestMethod]
        public void TestDelCartIsNotExisted()
        {
            var carts = CreateMockCart();
            var cart = CreateCart(Guid.NewGuid());

            _mockCartRepository.Setup(s => s.DelCart(It.IsAny<Cart>())).Callback((Cart cart) => carts.Remove(cart)).Verifiable();

            _mockCartRepository.Setup(s => s.IsExistCart(cart)).Returns(carts.Contains(cart));

            Assert.ThrowsException<Exception>(() => _cartService.DelCart(cart));
        }

        public void TestUpdateCartDefault()
        {
            var carts = CreateMockCart();
            var cart = carts[0];
            var cartId = cart.Id;
            _mockCartRepository.Setup(s => s.UpdateCart(It.IsAny<Cart>()))
                                .Callback((Cart cart) =>
                                {
                                    carts.Remove(item: carts.Find(e => e.Id == cart.Id)!);
                                    carts.Add(cart);
                                }).Verifiable();

            _mockCartRepository.Setup(s => s.GetCart(cartId)).Returns(carts.Find(e => e.Id == cartId)!);

            _cartService.UpdateCart(cart);

            compare(cart, _cartService.GetCartById(cartId));
        }

        [TestMethod]
        public void TestUpdateCartIsNotExisted()
        {
            var carts = CreateMockCart();
            var cart = carts[0];
            var cartId = Guid.NewGuid();
            _mockCartRepository.Setup(s => s.UpdateCart(It.IsAny<Cart>()))
                                .Callback((Cart Cart) =>
                                {
                                    carts.Remove(item: carts.Find(e => e.Id == Cart.Id)!);
                                    carts.Add(Cart);
                                }).Verifiable();

            _mockCartRepository.Setup(s => s.GetCart(cartId)).Returns(carts.Find(e => e.Id == cartId)!);

            Assert.ThrowsException<Exception>(() => _cartService.UpdateCart(cart));
        }

        private Cart CreateCart(Guid cartId)
        {
            return new Cart(cartId, "20/10/2025", Guid.NewGuid());
        }

        private List<Cart> CreateMockCart()
        {
            return new List<Cart>()
        {
            new Cart(Guid.NewGuid(),"21/11/2023",  Guid.NewGuid()),
            new Cart(Guid.NewGuid(), "11/10/2021", Guid.NewGuid()),
            new Cart(Guid.NewGuid(), "23/12/2022", Guid.NewGuid())};
        }
    }
}