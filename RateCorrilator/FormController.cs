using System.Collections.Generic;
using System.Web.Http;
using System;
using Newtonsoft.Json;

namespace RateCorrilator
{
    public class FormController : ApiController{

        public static readonly ErrorOutput err = new ErrorOutput { Pearson="Error, invalid dates"};
        public string Get(string from, string to){
            if (from == null || to == null) {
                return JsonConvert.SerializeObject(err);
            } else if (from.CompareTo(to) > 0) {
                return JsonConvert.SerializeObject(err);
            } else {
                double[] cadusdexh;
                double[] corra;
                UserOutput results = new UserOutput();
                Calculations.RequestData(from, to, out cadusdexh, out corra);
                if (corra.Length == 0 || cadusdexh.Length == 0) {
                    return JsonConvert.SerializeObject(err);
                } else {
                    Calculations.Bounds(cadusdexh,
                                       min => results.CadUsdExh_min = min,
                                       max => results.CadUsdExh_max = max,
                                       avg => results.CadUsdExh_avg = Math.Round(avg, 3));
                    Calculations.Bounds(corra,
                                           min => results.Corra_min = min,
                                           max => results.Corra_max = max,
                                           avg => results.Corra_avg = Math.Round(avg, 2));
                    Calculations.PearsonCor(cadusdexh, corra, p => results.Pearson = Math.Round(p, 2));

                    return JsonConvert.SerializeObject(results);
                }
            }
        }
        public void Post([FromBody] string value){
            
        }
    }
}
