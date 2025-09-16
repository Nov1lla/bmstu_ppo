using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Models
{
    public class UserPromo
    {
        private Guid id;
        private Guid id_user;
        private Guid id_promo;

        public Guid Id { get => id; set => id = value; }
        public Guid Id_user { get => id_user; set => id_user = value; }
        public Guid Id_promo { get => id_promo; set => id_promo = value; }

        public UserPromo(Guid id, Guid id_user, Guid id_promo)
        {
            Id = id;
            Id_user = id_user;
            Id_promo = id_promo;
        }
    }
}
