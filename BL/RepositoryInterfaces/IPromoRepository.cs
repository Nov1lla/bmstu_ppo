using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BL.Models;

namespace BL.RepositoryInterfaces
{
    public interface IPromoRepository
    {
        Promo GetPromo(int id);
        void AddPromo(Promo Promo);
        void DelPromo(Promo Promo);
        void UpdatePromo(Promo Promo);

        bool IsExistPromo(Promo Promo);
    }
}
