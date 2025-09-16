using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BL.RepositoryInterfaces;
using BL.Models;
using BL.MyException;
using Microsoft.Extensions.Logging.Abstractions;

namespace DA
{
    public class ItemCartRepository : IItemCartRepository
    {
        private Connector connector;
        public Connector Connector { get => connector; set => connector = value; }
        public ItemCartRepository(Connector connect)
        {
            Connector = connect;
        }
        public ItemCart GetItemCart(int id)
        {
            CheckConnection.checkConnection(Connector);
            string query = queryGetItemCart(id);

            NpgsqlCommand cmd = new NpgsqlCommand(query, Connector.Connect);

            ItemCart itemCart = null;
            using (NpgsqlDataReader reader = cmd.ExecuteReader())
            {
                if (reader.Read())
                {
                    itemCart = new ItemCart(reader.GetInt32(0), reader.GetInt32(1), reader.GetInt32(2), reader.GetInt32(3));
                }
                reader.Close();
            }
            return itemCart;
        }
        public void AddItemCart(ItemCart itemCart)
        {
            CheckConnection.checkConnection(Connector);
            string query = queryAddItemCart(itemCart);
            NpgsqlCommand cmd = new NpgsqlCommand(query, Connector.Connect);
            cmd.ExecuteNonQuery();
        }
        public void DelItemCart(ItemCart itemCart)
        {
            CheckConnection.checkConnection(Connector);
            string query = queryDelItemCart(itemCart);
            NpgsqlCommand cmd = new NpgsqlCommand(query, Connector.Connect);
            cmd.ExecuteNonQuery();
        }
        public void UpdateItemCart(ItemCart itemCart)
        {
            CheckConnection.checkConnection(Connector);
            string query = queryUpdateItemCart(itemCart);
            NpgsqlCommand cmd = new NpgsqlCommand(query, Connector.Connect);
            cmd.ExecuteNonQuery();
        }

        public bool IsExistItemCart(ItemCart itemCart)
        {
            CheckConnection.checkConnection(Connector);
            string query = queryIsExistCart(itemCart);
            NpgsqlCommand cmd = new NpgsqlCommand(query, Connector.Connect);

            bool flag = (bool)cmd.ExecuteScalar();
            return flag;

        }

        public string queryAddItemCart(ItemCart itemCart)
        {
            return $"insert into ItemCartDB(id_product, id_cart, quantity)" +
                $" values ({itemCart.Id_product}, {itemCart.Id_cart}, {itemCart.Quantity})";
        }
        public string queryGetItemCart(int ID)
        {
            return $"select * from ItemCartDB where id = {ID}";
        }
        public string queryUpdateItemCart(ItemCart itemCart)
        {
            return $"update ItemCartDB " +
                $"set id_product = {itemCart.Id_product}, id_cart = {itemCart.Id_cart}, quantity = '{itemCart.Quantity}'" +
                $" where id = {itemCart.Id}";
        }
        public string queryDelItemCart(ItemCart itemCart)
        {
            return $"delete from ItemCartDB where id = {itemCart.Id}";
        }
        public string queryIsExistCart(ItemCart itemCart)
        {
            return $"select exists (select * from ItemCartDB where id = {itemCart.Id})";
        }
    }
}
