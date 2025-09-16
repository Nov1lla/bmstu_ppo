using DA;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace lab04_Test.DA
{
    public static class GetConnection
    {
        public static Connector GetConnector()
        {
            return new Connector("postgres", "Qazedctgb13", "localhost", "db_ppo", 5432);
        }
    }
}
