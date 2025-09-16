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
    public class UserPromoRepository : IUserPromoRepository
    {
        private Connector connector;
        public Connector Connector { get => connector; set => connector = value; }
        public UserPromoRepository(Connector connect)
        {
            Connector = connect;
        }
        public UserPromo GetUserPromo(int id)
        {
            CheckConnection.checkConnection(Connector);
            string query = queryGetUserPromo(id);

            NpgsqlCommand cmd = new NpgsqlCommand(query, Connector.Connect);

            UserPromo userPromo = null;
            using (NpgsqlDataReader reader = cmd.ExecuteReader())
            {
                if (reader.Read())
                {
                    userPromo = new UserPromo(reader.GetInt32(0), reader.GetInt32(1), reader.GetInt32(2));
                }
                reader.Close();
            }
            return userPromo;
        }

        public List<UserPromo> GetUserPromoByIdUser(int id_user)
        {
            CheckConnection.checkConnection(Connector);
            string query = queryGetUserPromoByIdUser(id_user);

            NpgsqlCommand cmd = new NpgsqlCommand(query, Connector.Connect);
            cmd.ExecuteReader();
            List<UserPromo> allUserPromo = new List<UserPromo>();

            using (NpgsqlDataReader reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    allUserPromo.Add(new UserPromo(reader.GetInt32(0), reader.GetInt32(1), reader.GetInt32(2)));
                }
                reader.Close();
            }

            return allUserPromo;
        }

        public List<UserPromo> GetUserPromoByIdPromo(int id_promo)
        {
            CheckConnection.checkConnection(Connector);
            string query = queryGetUserPromoByIdPromo(id_promo);

            NpgsqlCommand cmd = new NpgsqlCommand(query, Connector.Connect);
            cmd.ExecuteReader();
            List<UserPromo> allUserPromo = new List<UserPromo>();

            using (NpgsqlDataReader reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    allUserPromo.Add(new UserPromo(reader.GetInt32(0), reader.GetInt32(1), reader.GetInt32(2)));
                }
                reader.Close();
            }

            return allUserPromo;
        }

        public void AddUserPromo(UserPromo userPromo)
        {
            CheckConnection.checkConnection(Connector);
            string query = queryAddUserPromo(userPromo);
            NpgsqlCommand cmd = new NpgsqlCommand(query, Connector.Connect);
            cmd.ExecuteNonQuery();
        }

        public void UpdateUserPromo(UserPromo userPromo)
        {
            CheckConnection.checkConnection(Connector);
            string query = queryUpdateUserPromo(userPromo);
            NpgsqlCommand cmd = new NpgsqlCommand(query, Connector.Connect);
            cmd.ExecuteNonQuery();
        }
        public void DelUserPromo(UserPromo userPromo)
        {
            CheckConnection.checkConnection(Connector);
            string query = queryDelUserPromo(userPromo);
            NpgsqlCommand cmd = new NpgsqlCommand(query, Connector.Connect);
            cmd.ExecuteNonQuery();
        }

        public bool IsExistUserPromo(UserPromo userPromo)
        {
            CheckConnection.checkConnection(Connector);
            string query = queryIsExistUserPromo(userPromo);
            NpgsqlCommand cmd = new NpgsqlCommand(query, Connector.Connect);

            bool flag = (bool)cmd.ExecuteScalar();
            return flag;
        }
        public string queryGetUserPromo(int ID)
        {
            return $"select * from UserPromoDB where id = {ID}";
        }
        public string queryGetUserPromoByIdUser(int id_user)
        {
            return $"select * from UserPromoDB where id_user = {id_user}";
        }
        public string queryGetUserPromoByIdPromo(int id_promo)
        {
            return $"select * from UserPromoDB where id_user = {id_promo}";
        }
        public string queryAddUserPromo(UserPromo userPromo)
        {
            return $"insert into UserPromoDB(id_user, id_promo)" +
                $" values ('{userPromo.Id_user}', '{userPromo.Id_promo}')";
        }
        public string queryUpdateUserPromo(UserPromo userPromo)
        {
            return $"update UserPromoDB " +
                $"set id_user = '{userPromo.Id_user}', id_promo = '{userPromo.Id_promo}'" +
                $" where id = {userPromo.Id}";
        }
        public string queryDelUserPromo(UserPromo userPromo)
        {
            return $"delete from UserPromoDB where id = {userPromo.Id}";
        }

        public string queryIsExistUserPromo(UserPromo userPromo)
        {
            return $"select exists (select * from UserPromoDB where id = {userPromo.Id}";
        }

    }
}
