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
using System.Data;

namespace DA
{
    public class UserRepository : IUserRepository
    {
        private Connector connector;
        public Connector Connector { get => connector; set => connector = value; }
        public UserRepository(Connector connect)
        {
            Connector = connect;
        }
        public User GetUser(int id)
        {
            CheckConnection.checkConnection(Connector);
            string query = queryGetUser(id);

            NpgsqlCommand cmd = new NpgsqlCommand(query, Connector.Connect);

            User user = null;

            NpgsqlDataReader reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                user = new User(reader.GetInt32(0), reader.GetString(1), reader.GetString(2),
                        reader.GetString(3), reader.GetString(4), reader.GetString(5), reader.GetString(6), reader.GetString(7));
            }
            reader.Close();
            return user;
        }

    public User GetUser(string login)
    {
            CheckConnection.checkConnection(Connector);
            string query = queryGetUser(login);

            NpgsqlCommand cmd = new NpgsqlCommand(query, Connector.Connect);

            User user = null;
            NpgsqlDataReader reader = cmd.ExecuteReader();
            
            if (reader.Read())
            {
                user = new User(reader.GetInt32(0), reader.GetString(1), reader.GetString(2),
                    reader.GetString(3), reader.GetString(4), reader.GetString(5), reader.GetString(6), reader.GetString(7));
            }
            reader.Close();
            
            return user;
        }

        public void AddUser(User user)
        {
            CheckConnection.checkConnection(Connector);
            string query = queryAddUser(user);
            NpgsqlCommand cmd = new NpgsqlCommand(query, Connector.Connect);
            cmd.ExecuteNonQuery();
        }
        public void DelUser(User user)
        {
            CheckConnection.checkConnection(Connector);
            string query = queryDelUser(user);
            NpgsqlCommand cmd = new NpgsqlCommand(query, Connector.Connect);
            cmd.ExecuteNonQuery();
        }
        public void UpdateUser(User user)
        {
            CheckConnection.checkConnection(Connector);
            string query = queryUpdateUser(user);
            NpgsqlCommand cmd = new NpgsqlCommand(query, Connector.Connect);
            cmd.ExecuteNonQuery();
        }

        public List<User> GetAll()
        {
            CheckConnection.checkConnection(Connector);
            string query = queryGetAll();
            List<User> allUser = new List<User>();
            NpgsqlCommand cmd = new NpgsqlCommand(query, Connector.Connect);
            using (NpgsqlDataReader reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    allUser.Add(new User(reader.GetInt32(0), reader.GetString(1), reader.GetString(2),
                        reader.GetString(3), reader.GetString(4), reader.GetString(5), reader.GetString(6), reader.GetString(7)));                  
                }
                reader.Close();
            }
            return allUser;
        }

        public int CountAllUsers()
        {
            CheckConnection.checkConnection(Connector);
            string query = queryCountAllUsers();
       
            NpgsqlCommand cmd = new NpgsqlCommand(query, Connector.Connect);

            return Convert.ToInt32(cmd.ExecuteScalar());
        }
        public string queryGetUser(int ID)
        {
            return $"select * from UserDB where id = {ID}";
        }

        public string queryGetUser(string login)
        {
            return $"select * from UserDB where login = '{login}'";
        }
        public string queryAddUser(User obj)
        {
            return $"insert into UserDB(name, phone, address, email, login, password, role)" +
                $" values ('{obj.Name}', '{obj.Phone}', '{obj.Address}', '{obj.Email}', '{obj.Login}', '{obj.Password}', '{obj.Role}')";
        }
        public string queryDelUser(User user)
        {
            return $"delete from UserPromoDB where id_user = {user.Id}; delete from UserDB where id = {user.Id}";
        }

        public string queryUpdateUser(User user)
        {
            return $"update UserDB " +
                $"set name = '{user.Name}', phone = '{user.Phone}', address = '{user.Address}', email = '{user.Email}', login = '{user.Login}', password = '{user.Password}', role = '{user.Role}'" +
                $" where id = {user.Id}";
        }
        public string queryGetAll()
        {
            return $"select * from UserDB";
        }

        public string queryCountAllUsers()
        {
            return "select COUNT(*) from UserDB";
        }

    }
}
