using BL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.RepositoryInterfaces
{
    public interface IProductRepository
    {
        Product GetProduct(int id);
        Product GetProduct(string name);
        void AddProduct(Product product);
        void DelProduct(Product product);

        void UpdateProduct(Product product);

        bool IsExistProduct(Product product);

    }
}