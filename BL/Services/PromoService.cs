using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BL.Models;
using BL.RepositoryInterfaces;

namespace BL.Services
{
    public class PromoService
    {
        private readonly IPromoRepository _promoRepository;
        private readonly IUserRepository _userRepository;
        private readonly IUserPromoRepository _userPromoRepository;

        public PromoService (IPromoRepository promoRepository, IUserRepository userRepository, IUserPromoRepository userPromoRepository)
        {
            _promoRepository = promoRepository;
            _userRepository = userRepository;
            _userPromoRepository = userPromoRepository;
        }
        public Promo GetPromo(int id)
        {
            return _promoRepository.GetPromo(id);
        }

        public List<Promo> GetPromoByIdUser(int id)
        {
            List<UserPromo> userPromo = _userPromoRepository.GetUserPromoByIdUser(id);
            List<Promo> promoList = new List<Promo>();
            if (userPromo.Count != 0)
            {
                foreach (UserPromo userPromoItem in userPromo)
                {
                    promoList.Add(_promoRepository.GetPromo(userPromoItem.Id_promo));
                }
            }
            return promoList;
        }

        public void AddPromo(Promo promo)
        {
            if (_promoRepository.IsExistPromo(promo) == true)
            {
                throw new Exception("Promo is existed");
            }
            else
                _promoRepository.AddPromo(promo);
        }
        public void UpdatePromo(Promo promo)
        {
            if (_promoRepository.IsExistPromo(promo) == false)
            {
                throw new Exception("Promo isn't existed");
            }
            else
                _promoRepository.UpdatePromo(promo);
        }
        public void DelPromo(Promo promo)
        {
            if (_promoRepository.IsExistPromo(promo) == false)
            {
                throw new Exception("Promo isn't existed");
            }
            else
                _promoRepository.DelPromo(promo);
        }
    }
}
