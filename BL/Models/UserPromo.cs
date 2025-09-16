using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Models
{
    public class UserPromo
    {
        private int id;
        private int id_user;
        private int id_promo;

        public int Id { get => id; set => id = value; }
        public int Id_user { get => id_user; set => id_user = value; }
        public int Id_promo { get => id_promo; set => id_promo = value; }

        public UserPromo(int id, int id_user, int id_promo)
        {
            Id = id;
            Id_user = id_user;
            Id_promo = id_promo;
        }
    }
}
