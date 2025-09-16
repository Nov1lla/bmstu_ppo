using BL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.RepositoryInterfaces
{
    public interface IItemCartRepository
    {
        ItemCart GetItemCart(int id);

        void AddItemCart(ItemCart itemCart);

        void DelItemCart(ItemCart itemCart);

        void UpdateItemCart(ItemCart itemCart);

        bool IsExistItemCart(ItemCart itemCart);
    }
}
