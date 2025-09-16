using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BL.Models;
using BL.RepositoryInterfaces;

namespace BL.Services
{
    public class ItemCartService
    {
        private readonly IItemCartRepository _itemCartRepository;

        public ItemCartService (IItemCartRepository itemCartRepository)
        {
            _itemCartRepository = itemCartRepository;   
        }
        public ItemCart GetItemCartById(Guid id)
        {
            return _itemCartRepository.GetItemCart(id);
        }

        public void AddItemCart(ItemCart itemCart)
        {
            if (_itemCartRepository.IsExistItemCart(itemCart) == true)
            {
                throw new Exception("Элемент корзины существует");
            }
            else
                _itemCartRepository.AddItemCart(itemCart);
        }

        public void DelItemCart(ItemCart itemCart)
        {
            if (_itemCartRepository.IsExistItemCart(itemCart) == false)
            {
                throw new Exception("Элемент корзины не существует");
            }
            else
                _itemCartRepository.DelItemCart(itemCart);  
        }

        public void UpdateItemCart(ItemCart itemCart)
        {
            if (_itemCartRepository.IsExistItemCart(itemCart) == false)
            {
                throw new Exception("Элемент корзины не существует");
            }
            else
                _itemCartRepository.UpdateItemCart(itemCart);
        }
    }
}
