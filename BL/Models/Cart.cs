using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Models
{
    public class Cart
    {
        private int id;
        private DateTime data_created;
        private int id_user;

        public int Id { get => id; set => id = value; }
        public DateTime Data_create { get => data_created; set => data_created = value; }
        public int Id_user { get => id_user; set => id_user = value; }

        public Cart(int id, DateTime data_created, int id_user)
        {
            this.id = id;
            this.data_created = data_created;
            this.id_user = id_user;
        }
    }
}
