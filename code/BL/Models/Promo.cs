using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Models
{
    public class Promo
    {
        private Guid id;
        private string code;
        private int discount;
        private string data_start;
        private string data_end;

        public Guid Id { get => id; set => id = value; }
        public string Code { get => code; set => code = value; }
        public int Discount { get => discount; set => discount = value; }
        public string Data_start { get => data_start; set => data_start = value; }
        public string Data_end { get => data_end; set => data_end = value; }


        public Promo(Guid id, string code, int discount, string data_start, string data_end)
        {
            Id = id;
            Code = code;
            Discount = discount;
            Data_start = data_start;
            Data_end = data_end;
        }
    }
}
