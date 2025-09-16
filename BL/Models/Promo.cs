using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Models
{
    public class Promo
    {
        private int id;
        private string code;
        private int discount;
        private DateTime data_start;
        private DateTime data_end;

        public int Id { get => id; set => id = value; }
        public string Code { get => code; set => code = value; }
        public int Discount { get => discount; set => discount = value; }
        public DateTime Data_start { get => data_start; set => data_start = value; }
        public DateTime Data_end { get => data_end; set => data_end = value; }


        public Promo(int id, string code, int discount, DateTime data_start, DateTime data_end)
        {
            Id = id;
            Code = code;
            Discount = discount;
            Data_start = data_start;
            Data_end = data_end;
        }
    }
}
