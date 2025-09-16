using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Models
{
    public class ItemOrder
    {
        private Guid id;
        private Guid id_product;
        private Guid id_order;
        private int quantity;

        public Guid Id { get => id; set => id = value; }
        public Guid Id_product { get => id_product; set => id_product = value; }
        public Guid Id_order { get => id_order; set => id_order = value; }
        public int Quantity { get => quantity; set => quantity = value; }


        public ItemOrder(Guid id, Guid id_product, Guid id_order, int quantity)
        {
            this.id = id;
            this.id_product = id_product;
            this.id_order = id_order;
            this.quantity = quantity;
        }
    }
}
