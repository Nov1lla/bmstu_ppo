using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Models
{
    public class ItemCart
    {
        private Guid id;
        private Guid id_product;
        private Guid id_cart;
        private int quantity;

        public Guid Id { get => id; set => id = value; }
        public Guid Id_product { get => id_product; set => id_product = value; }
        public Guid Id_cart { get => id_cart; set => id_cart = value; }
        public int Quantity { get => quantity; set => quantity = value; }

        public ItemCart(Guid id, Guid id_product, Guid id_cart, int quantity)
        {
            this.id = id;
            this.id_product = id_product;
            this.id_cart = id_cart;
            this.quantity = quantity;
        }
    }
}
