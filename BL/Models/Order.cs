using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Models
{
    public class Order
    {
        private int id;
        private string status;
        private DateTime data_created;
        private int id_user;
        private int id_promo;

        public int Id { get => id; set => id = value; }
        public string Status { get => status; set => status = value; }

        public DateTime Data_created { get => data_created; set => data_created = value; }
        public int Id_user { get => id_user; set => id_user = value; }
        public int Id_promo { get => id_promo; set => id_promo = value; }

        public Order(int id, string status, DateTime data_created, int id_user, int id_promo)
        {
            this.id = id;
            this.status = status;
            this.data_created = data_created;
            this.id_user = id_user;
            this.id_promo = id_promo;
        }
    }
}
