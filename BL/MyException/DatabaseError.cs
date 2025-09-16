using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.MyException
{
    public class DatabaseError : Exception
    {
        public DatabaseError() { }
        public DatabaseError(string message) : base(message) { }

    }
}
