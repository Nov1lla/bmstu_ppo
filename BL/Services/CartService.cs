using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BL.Models;
using BL.RepositoryInterfaces;

namespace BL.Services
{
    public class CartService
    {
        private readonly ICartRepository _cartRepository;

        public CartService(ICartRepository cartRepository)
        {
            _cartRepository = cartRepository ?? throw new Exception("Параметр пустой!");
        }

        public Cart GetCartById(int id)
        {
            return _cartRepository.GetCart(id);
        }

        public void AddCart(Cart cart)
        {
            if (_cartRepository.IsExistCart(cart) == true)
            {
                throw new Exception("Cart existed");
            }
            else
            {
                _cartRepository.AddCart(cart);
            }
        }

        public void DelCart(Cart cart)
        {
            if (_cartRepository.IsExistCart(cart) == false)
            {
                throw new Exception("Cart didnt exist");
            }
            else
            {
                _cartRepository.DelCart(cart);
            }
        }

        public void UpdateCart(Cart cart) 
        {
            if (_cartRepository.IsExistCart(cart) == false)
            {
                throw new Exception("Cart didn't exist");
            }
            else
            {
                _cartRepository.UpdateCart(cart);
            }
        }
    }
}
