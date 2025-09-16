using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Models
{
    public class User
    {
        private int id;
        private string name;
        private string phone;
        private string address;
        private string email;
        private string login;
        private string password;
        private string role;

        public int Id { get => id; set => id = value; }
        public string Name { get => name; set => name = value; }
        public string Phone { get => phone; set => phone = value; }
        public string Address { get => address; set => address = value; }
        public string Email { get => email; set => email = value; }
        public string Password { get => password; set => password = value; }
        public string Login { get => login; set => login = value; }
        public string Role { get => role; set => role = value; }

        public User(int id, string name, string phone, string address, string email, string login, string password, string role)
        {
            this.id = id;
            this.name = name;
            this.phone = phone;
            this.address = address;
            this.email = email;
            this.password = password;
            this.role = role;
            this.login = login;
        }

        public User(string name, string phone, string address, string email, string login, string password, string role)
        {
            this.name = name;
            this.phone = phone;
            this.address = address;
            this.email = email;
            this.password = password;
            this.role = role;
            this.login = login;
        }

    }
}
