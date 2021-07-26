using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TReEDeCor.Models
{
    public class ChiTiet
    {
        DatabaseDataContext data = new DatabaseDataContext();
        public int madh { get; set; }
        public int masp { get; set; }
        public int solong { get; set; }
        public decimal dongia { get; set; }
        public decimal tonggia { get; set; }

        public List<ChiTiet> listDe = new List<ChiTiet>();
    }
}