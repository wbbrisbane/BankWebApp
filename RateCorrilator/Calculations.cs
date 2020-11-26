using System;
using Microsoft.Owin.Hosting;
using System.Linq;
using System.Net.Http;
using System.Xml.Linq;

namespace RateCorrilator
{
    public class Calculations
    {
        public static readonly string baseURL_BoC = "https://www.bankofcanada.ca/valet";
        
        public static void RequestData(string start, string end, out double[] cadusdexh, out double[] corra){
            var responce = new HttpResponseMessage();
            XDocument CadUsdXML;
            XDocument CORRAXML;

            HttpClient client_BoC = new HttpClient();
            responce = client_BoC.GetAsync(baseURL_BoC + "/observations/FXUSDCAD/xml").Result;
            CadUsdXML = XDocument.Load(responce.Content.ReadAsStreamAsync().Result);
            responce = client_BoC.GetAsync(baseURL_BoC + "/observations/AVG.INTWO/xml").Result;
            CORRAXML = XDocument.Load(responce.Content.ReadAsStreamAsync().Result);
            var queryEX = from o in CadUsdXML.Root.Element("observations").Descendants()
                          where o.FirstAttribute.Value.CompareTo(start)>0 && o.FirstAttribute.Value.CompareTo(end)<0
                          select o.Value;
            var queryRA = from o in CORRAXML.Root.Element("observations").Descendants()
                          where o.FirstAttribute.Value.CompareTo(start)>0 && o.FirstAttribute.Value.CompareTo(end)<0
                          select o.Value;

            cadusdexh = new double[queryEX.Count()];
            corra = new double[queryRA.Count()];

            foreach (var item in queryEX.Select((value, i) => new { i,value })) {
                cadusdexh[item.i] = double.Parse(item.value);
            }
            foreach (var item in queryRA.Select((value, i) => new { i, value }))
            {
                corra[item.i] = double.Parse(item.value);
            }
        }

        public static void Bounds(double[] data, Action<double> min, Action<double> max, Action<double> avg) { 
            min(data.Min());
            max(data.Max());
            avg(data.Average());
        }

        public static void PearsonCor(double[] datax, double[] datay, Action<double> coeff) {
            double xbar = datax.Average();
            double ybar = datay.Average();
            double numerator = 0;
            double denomx = 0;
            double denomy = 0;

            for(int i=0; i < datax.Count(); i++){
                numerator += (datax[i]-xbar) * (datay[i]-ybar);
                denomx += System.Math.Pow((datax[i]-xbar),2);
                denomy += System.Math.Pow((datay[i] - ybar), 2);
            }

            coeff(numerator/System.Math.Sqrt(denomx*denomy));
        }
    }
}
