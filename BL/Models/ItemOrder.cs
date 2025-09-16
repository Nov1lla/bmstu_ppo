using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Models
{
    public class ItemOrder
    {
        private int id;
        private int id_product;
        private int id_order;
        private int quantity;

        public int Id { get => id; set => id = value; }
        public int Id_product { get => id_product; set => id_product = value; }
        public int Id_order { get => id_order; set => id_order = value; }
        public int Quantity { get => quantity; set => quantity = value; }


        public ItemOrder(int id, int id_product, int id_order, int quantity)
        {
            this.id = id;
            this.id_product = id_product;
            this.id_order = id_order;
            this.quantity = quantity;
        }
    }
}
