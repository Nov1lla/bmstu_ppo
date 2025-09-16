using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Models
{
    public class Product
    {
        private int id;
        private string name;
        private int price;
        private int quantity;
        private string manufacturer;
        private string description;

        public int Id { get => id; set => id = value; }
        public string Name { get => name; set => name = value; }
        public string Description { get => description; set => description = value; }
        public int Price { get => price; set => price = value; }
        public int Quantity { get => quantity; set => quantity = value; }
        public string Manufacturer { get => manufacturer; set => manufacturer = value; }

        public Product(int id, string name, int price, int quantity, string manufacturer, string description)
        {
            this.id = id;
            this.name = name;
            this.price = price;
            this.quantity = quantity;
            this.manufacturer = manufacturer;
            this.description = description;
        }

    }
}
