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
    public class ItemOrderRepository : IItemOrderRepository
    {
        private Connector connector;
        public Connector Connector { get => connector; set => connector = value; }
        public ItemOrderRepository(Connector connect)
        {
            Connector = connect;
        }
        public ItemOrder GetItemOrder(int id)
        {
            CheckConnection.checkConnection(Connector);
            string query = queryGetItemOrder(id);

            NpgsqlCommand cmd = new NpgsqlCommand(query, Connector.Connect);

            ItemOrder itemOrder = null;
            using (NpgsqlDataReader reader = cmd.ExecuteReader())
            {
                if (reader.Read())
                {
                    itemOrder = new ItemOrder(reader.GetInt32(0), reader.GetInt32(1), reader.GetInt32(2), reader.GetInt32(3));
                }
                reader.Close();
            }
            return itemOrder;
        }
        public void AddItemOrder(ItemOrder itemOrder)
        {
            CheckConnection.checkConnection(Connector);
            string query = queryAddItemOrder(itemOrder);
            NpgsqlCommand cmd = new NpgsqlCommand(query, Connector.Connect);
            cmd.ExecuteNonQuery();
        }
        public void DelItemOrder(ItemOrder itemOrder)
        {
            CheckConnection.checkConnection(Connector);
            string query = queryDelItemOrder(itemOrder);
            NpgsqlCommand cmd = new NpgsqlCommand(query, Connector.Connect);
            cmd.ExecuteNonQuery();
        }
        public void UpdateItemOrder(ItemOrder itemOrder)
        {
            CheckConnection.checkConnection(Connector);
            string query = queryUpdateItemOrder(itemOrder);
            NpgsqlCommand cmd = new NpgsqlCommand(query, Connector.Connect);
            cmd.ExecuteNonQuery();
        }

        public bool IsExistItemOrder(ItemOrder itemOrder)
        {
            CheckConnection.checkConnection(Connector);
            string query = queryIsExistOrder(itemOrder);
            NpgsqlCommand cmd = new NpgsqlCommand(query, Connector.Connect);

            bool flag = (bool)cmd.ExecuteScalar();
            return flag;
        }

        public string queryAddItemOrder(ItemOrder itemOrder)
        {
            return $"insert into ItemOrderDB(id_product, id_order, quantity)" +
                $" values ({itemOrder.Id_product}, {itemOrder.Id_order}, {itemOrder.Quantity})";
        }
        public string queryGetItemOrder(int ID)
        {
            return $"select * from ItemOrderDB where id = {ID}";
        }
        public string queryUpdateItemOrder(ItemOrder itemOrder)
        {
            return $"update ItemOrderDB " +
                $"set id_product = {itemOrder.Id_product}, id_order = {itemOrder.Id_order}, quantity = '{itemOrder.Quantity}'" +
                $" where id = {itemOrder.Id}";
        }
        public string queryDelItemOrder(ItemOrder itemOrder)
        {
            return $"delete from ItemOrderDB where id = {itemOrder.Id}";
        }
        public string queryIsExistOrder(ItemOrder itemOrder)
        {
            return $"select exists (select * from ItemOrderDB where id = {itemOrder.Id})";
        }
    }
}
