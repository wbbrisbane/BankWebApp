using System;
using Microsoft.Owin.Hosting;
using System.Linq;
using System.Net.Http;
using System.Xml.Linq;

namespace RateCorrilator
{
    public class Program
    {
        public static readonly string baseURL_User = "http://localhost:8000/";
        static void Main(string[] args)
        {
            WebApp.Start<Startup>(new StartOptions(baseURL_User) { ServerFactory = "Microsoft.Owin.Host.HttpListener" });
            System.Diagnostics.Process.Start(baseURL_User);
            Console.Write("Hit <Enter> to end.");
            Console.ReadLine();
        }
    }
}
