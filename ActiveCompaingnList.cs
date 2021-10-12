using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DonateKart
{
    public class ActiveCompaingnList
    {
        public string Title { get; set; }
        public double TotalAmount { get; set; }

        public double backersCount { get; set; }

        public DateTime EndDate { get; set; }

        public DateTime created { get; set; }
    }
}
