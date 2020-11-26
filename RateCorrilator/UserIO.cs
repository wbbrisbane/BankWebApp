using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RateCorrilator
{
    public class UserOutput{
        public double CadUsdExh_min { get; set; }
        public double CadUsdExh_max { get; set; }
        public double CadUsdExh_avg { get; set; }
        public double Corra_min { get; set; }
        public double Corra_max { get; set; }
        public double Corra_avg { get; set; }
        public double Pearson { get; set; }
    }

    public class ErrorOutput{
        public double CadUsdExh_min { get; set; }
        public double CadUsdExh_max { get; set; }
        public double CadUsdExh_avg { get; set; }
        public double Corra_min { get; set; }
        public double Corra_max { get; set; }
        public double Corra_avg { get; set; }
        public string Pearson { get; set; }
    }
}
