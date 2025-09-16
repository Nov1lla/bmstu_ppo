using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Models
{
    public class ItemCart
    {
        private int id;
        private int id_product;
        private int id_cart;
        private int quantity;

        public int Id { get => id; set => id = value; }
        public int Id_product { get => id_product; set => id_product = value; }
        public int Id_cart { get => id_cart; set => id_cart = value; }
        public int Quantity { get => quantity; set => quantity = value; }

        public ItemCart(int id, int id_product, int id_cart, int quantity)
        {
            this.id = id;
            this.id_product = id_product;
            this.id_cart = id_cart;
            this.quantity = quantity;
        }
    }
}
