using SimpleBrowser;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HttpModemAutomation
{
    class Program
    {
        static void Main(string[] args)

        {
           var modem = new Browser();
            var modemURL = "http://192.168.20.1:8080";
                modem.Navigate(modemURL);
            //    Console.Write("get url");
            //    modem.BasicAuthenticationLogin("Broadband Router","admin","admin");
            modem.BasicAuthenticationLogin("192.168.20.1", "admin", "admin");


            modem.Navigate(modemURL);
                Console.Write(modem.CurrentHtml);
           // Console.Write(modem.)
            Console.Read();


        }
    }
}
