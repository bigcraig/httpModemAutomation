using SimpleBrowser;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace HttpModemAutomation
{
    class Program
    {
         static string getSessionID(string html) {      // static since we dont need to instance another program class to run it

            string _pattern = @"var sessionKey='([^']+)';";
            Match match = Regex.Match(html, _pattern);
            return  match.Groups[1].Value;

        }

        static void Main(string[] args)

        {
           var modem = new Browser();
            var modemURL = "http://192.168.20.1:8080/tr69cfg.html";
                modem.Navigate(modemURL);
            //    Console.Write("get url");
            //    modem.BasicAuthenticationLogin("Broadband Router","admin","admin");
            modem.BasicAuthenticationLogin("192.168.20.1", "admin", "admin");


            modem.Navigate(modemURL);
                Console.Write(modem.CurrentHtml);
            var html = modem.CurrentHtml;

            string pattern = @"var sessionKey='([^']+)';";
            Match match = Regex.Match(html, pattern);
            string sessionKey = match.Groups[1].Value;
            Console.Write(sessionKey);
            sessionKey = getSessionID(html);
            Console.Write(sessionKey);
            Console.Read();


        }
    }
}
