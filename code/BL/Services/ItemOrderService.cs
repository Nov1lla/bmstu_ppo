using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BL.Models;
using BL.RepositoryInterfaces;

namespace BL.Services
{
    public class ItemOrderService
    {
        private readonly IItemOrderRepository _itemOrderRepository;

        public ItemOrderService(IItemOrderRepository itemOrderRepository)
        {
            _itemOrderRepository = itemOrderRepository;
        }

        public ItemOrder GetItemOrderById(Guid id)
        {
            return _itemOrderRepository.GetItemOrder(id);
        }

        public void AddItemOrder(ItemOrder itemOrder)
        {
            if (_itemOrderRepository.IsExistItemOrder(itemOrder) == true)
            {
                throw new Exception("Элемент корзины существует");
            }
            else
                _itemOrderRepository.AddItemOrder(itemOrder);
        }

        public void DelItemOrder(ItemOrder itemOrder)
        {
            if (_itemOrderRepository.IsExistItemOrder(itemOrder) == false)
            {
                throw new Exception("Элемент корзины не существует");
            }
            else
                _itemOrderRepository.DelItemOrder(itemOrder);
        }

        public void UpdateItemOrder(ItemOrder itemOrder)
        {
            if (_itemOrderRepository.IsExistItemOrder(itemOrder) == false)
            {
                throw new Exception("Элемент корзины не существует");
            }
            else
                _itemOrderRepository.UpdateItemOrder(itemOrder);
        }
    }
}
