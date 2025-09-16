using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Models
{
    public class Cart
    {
        private Guid id;
        private string data_created;
        private Guid id_user;

        public Guid Id { get => id; set => id = value; }
        public string Data_create { get => data_created; set => data_created = value; }
        public Guid Id_user { get => id_user; set => id_user = value; }

        public Cart(Guid id, string data_created, Guid id_user)
        {
            this.id = id;
            this.data_created = data_created;
            this.id_user = id_user;
        }
    }
}
