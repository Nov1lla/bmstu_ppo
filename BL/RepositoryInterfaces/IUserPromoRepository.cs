using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BL.Models;

namespace BL.RepositoryInterfaces
{
    public interface IUserPromoRepository
    {
        UserPromo GetUserPromo(int id);
        List<UserPromo> GetUserPromoByIdUser(int id);
        List<UserPromo> GetUserPromoByIdPromo(int id);

        void AddUserPromo(UserPromo userPromo);
        void DelUserPromo(UserPromo userPromo);

        bool IsExistUserPromo(UserPromo userPromo);
    }
}
