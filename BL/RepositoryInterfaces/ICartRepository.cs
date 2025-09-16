using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BL.Models;


namespace BL.RepositoryInterfaces
{
    public interface ICartRepository
    {
        Cart GetCart(int id);
        void AddCart(Cart cart);
        void DelCart(Cart cart);
        void UpdateCart(Cart cart);

        bool IsExistCart(Cart cart);
    }
}
