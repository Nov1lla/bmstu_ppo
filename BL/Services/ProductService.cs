using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BL.Models;
using BL.RepositoryInterfaces;

namespace BL.Services
{
    public class ProductService
    {
        private readonly IProductRepository _productRepository;

        public ProductService(IProductRepository productRepository)
        {
            _productRepository = productRepository ?? throw new Exception("Параметр пустой!");
        }

        public Product GetProductById(int id)
        {
            return _productRepository.GetProduct(id);
        }

        public Product GetProductByName(string name)
        {
            return _productRepository.GetProduct(name);
        }

        public void AddProduct(Product product)
        {
            if (_productRepository.IsExistProduct(product) == true)
            {
                throw new Exception("Уже существует это продукт!");
            }
            else 
                _productRepository.AddProduct(product);
        }

        public void DelProduct(Product product)
        {
            if (_productRepository.IsExistProduct(product) == false)
            {
                throw new Exception("не существует это продукт!");
            }
            else
                _productRepository.DelProduct(product);
        }
        public void UpdateProduct(Product _product) 
        {
            Product product = _productRepository.GetProduct(_product.Id);
            if (product == null)
            {
                throw new Exception("нет такой продукт");
            }
            else
                _productRepository.UpdateProduct(product);
        }
    }
}
