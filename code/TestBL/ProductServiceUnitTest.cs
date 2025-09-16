using System;
using BL.Models;
using BL.RepositoryInterfaces;
using BL.Services;
using Moq;

namespace TestBL
{
    [TestClass]
    public class ProductServiceUnitTest
    {
        private readonly ProductService _productService;
        private readonly Mock<IProductRepository> _mockProductRepository = new();

        public ProductServiceUnitTest()
        {
            _productService = new ProductService(_mockProductRepository.Object);
        }
        [TestMethod]
        private void compare(Product x, Product y)
        {
            Assert.AreEqual(x.Name, y.Name);
            Assert.AreEqual(x.Price, y.Price);
            Assert.AreEqual(x.Manufacturer, y.Manufacturer);
            Assert.AreEqual(x.Description, y.Description);
            Assert.AreEqual(x.Quantity, y.Quantity);
        }


        [TestMethod]
        public void TestGetProductByIdDefault()
        {
            var products = CreateMockProduct();
            var productId = products[0].Id;

            _mockProductRepository.Setup(s => s.GetProduct(productId)).Returns(products.Find(e => e.Id == productId)!);


            Product productFound = _productService.GetProductById(productId);
            compare(products[0], productFound);
        }

        [TestMethod]
        public void TestGetProductByIdNotFound()
        {
            var products = CreateMockProduct();
            var productId = Guid.NewGuid();

            _mockProductRepository.Setup(s => s.GetProduct(productId)).Returns(products.Find(e => e.Id == productId)!);


            Product productFound = _productService.GetProductById(productId);
            Assert.IsNull(productFound);
        }

        public void TestGetProductByNameDefault()
        {
            var products = CreateMockProduct();
            var productName = products[0].Name;

            _mockProductRepository.Setup(s => s.GetProduct(productName)).Returns(products.Find(e => e.Name == productName)!);


            Product productFound = _productService.GetProductByName(productName);
            compare(products[0], productFound);
        }

        [TestMethod]
        public void TestGetProductByNameNotFound()
        {
            var products = CreateMockProduct();
            var productName = "test";

            _mockProductRepository.Setup(s => s.GetProduct(productName)).Returns(products.Find(e => e.Name == productName)!);


            Product productFound = _productService.GetProductByName(productName);
            Assert.IsNull(productFound);
        }

        [TestMethod]
        public void TestAddProductDefault()
        {
            var products = CreateMockProduct();
            var length = products.Count();
            var productId = Guid.NewGuid();
            var product = CreateProduct(productId);

            _mockProductRepository.Setup(s => s.AddProduct(It.IsAny<Product>())).Callback((Product product) => products.Add(product)).Verifiable();
            
            _mockProductRepository.Setup(s => s.IsExistProduct(product)).Returns(products.Contains(product));

            _productService.AddProduct(product);

            Assert.AreEqual(length + 1, products.Count());
        }

        [TestMethod]
        public void TestAddProductIsExisted()
        {
            var products = CreateMockProduct();
            var product = products[0];

            _mockProductRepository.Setup(s => s.AddProduct(It.IsAny<Product>())).Callback((Product product) => products.Add(product)).Verifiable();

            _mockProductRepository.Setup(s => s.IsExistProduct(product)).Returns(products.Contains(product));

            Assert.ThrowsException<Exception>(() => _productService.AddProduct(product));
        }

        public void TestDelProductDefault()
        {
            var products = CreateMockProduct();
            var length = products.Count();
            var product = products[0];

            _mockProductRepository.Setup(s => s.DelProduct(It.IsAny<Product>())).Callback((Product product) => products.Remove(product)).Verifiable();

            _mockProductRepository.Setup(s => s.IsExistProduct(product)).Returns(products.Contains(product));

            _productService.DelProduct(product);

            Assert.AreEqual(length - 1, products.Count());
        }

        [TestMethod]
        public void TestDelProductIsNotExisted()
        {
            var products = CreateMockProduct();
            var product = CreateProduct(Guid.NewGuid());

            _mockProductRepository.Setup(s => s.DelProduct(It.IsAny<Product>())).Callback((Product product) => products.Remove(product)).Verifiable();

            _mockProductRepository.Setup(s => s.IsExistProduct(product)).Returns(products.Contains(product));

            Assert.ThrowsException<Exception>(() => _productService.DelProduct(product));
        }

        public void TestUpdateProductDefault()
        {
            var products = CreateMockProduct();
            var product = products[0];
            var productId = product.Id;
            product.Name = "test";
            _mockProductRepository.Setup(s => s.UpdateProduct(It.IsAny<Product>()))
                                .Callback((Product product) =>
                                {
                                    products.Remove(item: products.Find(e => e.Id == product.Id)!);
                                    products.Add(product);
                                }) .Verifiable();

            _mockProductRepository.Setup(s => s.GetProduct(productId)).Returns(products.Find(e => e.Id == productId)!);

            _productService.UpdateProduct(product);

            compare(product, _productService.GetProductById(productId));
        }

        [TestMethod]
        public void TestUpdateProductIsNotExisted()
        {
            var products = CreateMockProduct();
            var product = products[0];
            var productId = Guid.NewGuid();
            _mockProductRepository.Setup(s => s.UpdateProduct(It.IsAny<Product>()))
                                .Callback((Product product) =>
                                {
                                    products.Remove(item: products.Find(e => e.Id == product.Id)!);
                                    products.Add(product);
                                }).Verifiable();

            _mockProductRepository.Setup(s => s.GetProduct(productId)).Returns(products.Find(e => e.Id == productId)!);

            Assert.ThrowsException<Exception>(() => _productService.UpdateProduct(product));
        }

        private Product CreateProduct(Guid productId)
        {
            return new Product(productId, "Thinkpad", 1000, 10, "Lenovo", "made in China");
        }

        private List<Product> CreateMockProduct()
        {
            return new List<Product>()
        {
            new Product(Guid.NewGuid(), "Legion", 1200, 3, "Lenovo", "made in China"),
            new Product(Guid.NewGuid(), "Macbook", 1500, 2, "Apple", "USA"),
            new Product(Guid.NewGuid(), "Tab", 1200, 3, "SamSumg", "Hanoi")};
        }
    }
}