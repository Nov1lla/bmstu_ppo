using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BL.Models;

namespace BL.RepositoryInterfaces
{
    public interface IOrderRepository
    {
        Order GetOrder(Guid id);
        List<Order> GetAllOrders();
        void AddOrder(Order order);
        void DelOrder(Order order);

        void UpdateOrder(Order order);

        bool IsExistOrder(Order order);
    }
}
