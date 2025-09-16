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
    public class PromoRepository : IPromoRepository
    {
        private Connector connector;
        public Connector Connector { get => connector; set => connector = value; }
        public PromoRepository(Connector connect)
        {
            Connector = connect;
        }
        public Promo GetPromo(int id)
        {
            CheckConnection.checkConnection(Connector);
            string query = queryGetPromo(id);

            NpgsqlCommand cmd = new NpgsqlCommand(query, Connector.Connect);

            Promo promo = null;
            NpgsqlDataReader reader = cmd.ExecuteReader();
            
            if (reader.Read())
            {
                promo = new Promo(reader.GetInt32(0), reader.GetString(1), reader.GetInt32(2),
                    reader.GetDateTime(3), reader.GetDateTime(4));
            }
            reader.Close();
            
            return promo;
        }

        public void AddPromo(Promo promo)
        {
            CheckConnection.checkConnection(Connector);
            string query = queryAddPromo(promo);
            NpgsqlCommand cmd = new NpgsqlCommand(query, Connector.Connect);
            cmd.ExecuteNonQuery();
        }
        public void DelPromo(Promo promo)
        {
            CheckConnection.checkConnection(Connector);
            string query = queryDelPromo(promo);
            NpgsqlCommand cmd = new NpgsqlCommand(query, Connector.Connect);
            cmd.ExecuteNonQuery();
        }
        public void UpdatePromo(Promo promo)
        {
            CheckConnection.checkConnection(Connector);
            string query = queryUpdatePromo(promo);
            NpgsqlCommand cmd = new NpgsqlCommand(query, Connector.Connect);
            cmd.ExecuteNonQuery();
        }
        public bool IsExistPromo(Promo promo)
        {
            CheckConnection.checkConnection(Connector);
            string query = queryIsExistOrder(promo);
            NpgsqlCommand cmd = new NpgsqlCommand(query, Connector.Connect);

            bool flag = (bool)cmd.ExecuteScalar();
            return flag;
        }
        public string queryGetPromo(int ID)
        {
            return $"select * from PromoDB where id = {ID}";
        }

        public string queryGetPromo(string login)
        {
            return $"select * from PromoDB where login = {login}";
        }
        public string queryAddPromo(Promo promo)
        {
            return $"insert into PromoDB(code, discount, data_start, data_end)" +
                $" values ('{promo.Code}', {promo.Discount}, '{promo.Data_start}', '{promo.Data_end}')";
        }
        public string queryDelPromo(Promo promo)
        {
            return $"delete from PromoDB where id = {promo.Id}";
        }

        public string queryUpdatePromo(Promo promo)
        {
            return $"update PromoDB " +
                $"set code = '{promo.Code}', discount = {promo.Discount}, data_start = '{promo.Data_start}', data_end = '{promo.Data_end}'" +
                $" where id = {promo.Id}";
        }

        public string queryIsExistOrder(Promo promo)
        {
            return $"select exists (select * from PromoDB where id = {promo.Id}";
        }

    }
}
