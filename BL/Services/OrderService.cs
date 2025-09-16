using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BL.Models;
using BL.RepositoryInterfaces;

namespace BL.Services
{
    public class OrderService
    {
        private readonly IOrderRepository _orderRepository; 
        public OrderService (IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        public Order GetOrderById(int id)
        {
            return _orderRepository.GetOrder(id);
        }

        public void AddOrder(Order order)
        {
            if (_orderRepository.IsExistOrder(order) == true)
            {
                throw new Exception("Заказ существует");
            }
            else
                _orderRepository.AddOrder(order);
        }
        public void DelOrder(Order order)
        {   
            if (_orderRepository.IsExistOrder(order) != true)
            {
                throw new Exception("Заказ не существует");
            }
            else
                _orderRepository.DelOrder(order);
        }

        public void UpdateOrder(Order order)
        {
            if (_orderRepository.IsExistOrder(order) != true)
            {
                throw new Exception("Заказ не существует");
            }
            else
                _orderRepository.UpdateOrder(order);
        }
    }
}
