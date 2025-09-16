using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Models
{
    public class Order
    {
        private Guid id;
        private string status;
        private string data_created;
        private Guid id_user;
        private Guid id_promo;

        public Guid Id { get => id; set => id = value; }
        public string Status { get => status; set => status = value; }
        public string Data_created { get => data_created; set => data_created = value; }
        public Guid Id_user { get => id_user; set => id_user = value; }
        public Guid Id_promo { get => id_promo; set => id_promo = value; }

        public Order(Guid id, string status, string data_created, Guid id_user, Guid id_promo)
        {
            this.id = id;
            this.status = status;
            this.data_created = data_created;
            this.id_user = id_user;
            this.id_promo = id_promo;
        }
    }
}
