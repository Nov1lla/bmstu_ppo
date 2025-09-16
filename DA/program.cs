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
    public class Test
    {
        static void Main(string[] args)
        {
            // Display the number of command line arguments.
            Console.WriteLine(args.Length);
        }

    }
}
