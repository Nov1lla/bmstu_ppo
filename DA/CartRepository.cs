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
    public class CartRepository : ICartRepository
    {
        private Connector connector;
        public Connector Connector { get => connector; set => connector = value; }
        public CartRepository(Connector connect)
        {
            Connector = connect;
        }
        public Cart GetCart(int id)
        {
            CheckConnection.checkConnection(Connector);
            string query = queryGetCart(id);

            NpgsqlCommand cmd = new NpgsqlCommand(query, Connector.Connect);

            Cart cart = null;
            using (NpgsqlDataReader reader = cmd.ExecuteReader())
            {
                if (reader.Read())
                {
                    cart = new Cart(reader.GetInt32(0), reader.GetDateTime(1), reader.GetInt32(2));
                }
                reader.Close();
            }
            return cart;
        }

        public void AddCart(Cart cart)
        {
            CheckConnection.checkConnection(Connector);
            string query = queryAddCart(cart);
            NpgsqlCommand cmd = new NpgsqlCommand(query, Connector.Connect);
            cmd.ExecuteNonQuery();
        }

        public void UpdateCart(Cart cart)
        {
            CheckConnection.checkConnection(Connector);
            string query = queryUpdateCart(cart);
            NpgsqlCommand cmd = new NpgsqlCommand(query, Connector.Connect);
            cmd.ExecuteNonQuery();
        }
        public void DelCart(Cart cart)
        {
            CheckConnection.checkConnection(Connector);
            string query = queryDelCart(cart);
            NpgsqlCommand cmd = new NpgsqlCommand(query, Connector.Connect);
            cmd.ExecuteNonQuery();
        }

        public bool IsExistCart(Cart cart)
        {
            CheckConnection.checkConnection(Connector);
            string query = queryIsExistCart(cart);
            NpgsqlCommand cmd = new NpgsqlCommand(query, Connector.Connect);

            bool flag = (bool)cmd.ExecuteScalar();
            return flag;
        }
        public string queryGetCart(int ID)
        {
            return $"select * from CartDB where id = {ID}";
        }
        public string queryAddCart(Cart cart)
        {
            return $"insert into CartDB(data_created, id_user)" +
                $" values ('{cart.Data_create}', {cart.Id_user})";
        }
        public string queryUpdateCart(Cart cart)
        {
            return $"update CartDB " +
                $"set data_created = '{cart.Data_create}', id_user = {cart.Id_user}" +
                $" where id = {cart.Id}";
        }
        public string queryDelCart(Cart cart)
        {
            return $"delete from CartDB where id = {cart.Id}";
        }

        public string queryIsExistCart(Cart cart)
        {
            return $"select exists (select * from CartDB where id = {cart.Id})";
        }

    }
}
