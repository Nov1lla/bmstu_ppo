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
        UserPromo GetUserPromo(Guid id);
        List<UserPromo> GetUserPromoByIdUser(Guid id);
        List<UserPromo> GetUserPromoByIdPromo(Guid id);

        void AddUserPromo(UserPromo userPromo);
        void DelUserPromo(UserPromo userPromo);

        bool IsExistUserPromo(UserPromo userPromo);
    }
}
