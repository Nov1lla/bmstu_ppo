using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Npgsql;
using System.Data;
using BL.MyException;
using BL.Models;

namespace DA
{
    public class Connector
    {
        private string user;
        private string password;
        private string host;
        private string database;
        private int port;
        private NpgsqlConnection connect;

        public string User { get { return user; } set { user = value; } }
        public string Password { get { return password; } set { password = value; } }
        public string Host { get { return host; } set { host = value; } }
        public string Database { get { return database; } set { database = value; } }
        public int Port { get { return port; } set { port = value; } }
        public NpgsqlConnection Connect { get { return connect; } set { connect = value; } }

        public Connector(string user, string password, string host, string database, int port)
        {
            User = user;
            Password = password;
            Host = host;
            Database = database;
            Port = port;
            Console.WriteLine(this.get_command_connect());
            Connect = new NpgsqlConnection(this.get_command_connect());
            Connect.Open();
            if (Connect == null || Connect.State != ConnectionState.Open)
                throw new DatabaseError("Can't open database!!!");
        }

        public Connector(Connector args)
        {
            User = args.User;
            Password = args.Password;
            Host = args.Host;
            Database = args.Database;
            Port = args.Port;
            Connect = args.Connect;
            Connect.Open();
            if (Connect == null || Connect.State != ConnectionState.Open) throw new DatabaseError("Can't open database!!!");

        }
        public string get_command_connect()
        {
            return "Host = " + Host + "; Username = " + User + "; Password = " + Password + "; Database = " + Database + ";";
        }
    }

    public static class CheckConnection
    {
        public static void checkConnection(Connector connector)
        {
            if (connector == null || connector.Connect.State != ConnectionState.Open) throw new DatabaseError("Database didnt open!");
        }
    }
}
