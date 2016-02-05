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

          static string  ACSConfigString(string sessionKey)
             /* string Tr69cAcsPwd,
              string tr69cConnReqUser,
              string tr69cConnReqPwd,
              string tr69cConnReqPort,
              string tr69cAcsUrl,
              string informInterval) */
        {
            string _acspost;
            _acspost = "tr69cfg.cgi?tr69cEnable=1&tr69cInformEnable=1&tr69cInformInterval=86423&tr69cAcsURL=http://acs.netcommwireless.com/cpe.php&tr69cAcsUser=cpe&tr69cAcsPwd=cpe&tr69cConnReqUser=cpe&tr69cConnReqPwd=cpe&tr69cConnReqPort=30005&tr69cNoneConnReqAuth=0&tr69cDebugEnable=0&tr69cBoundIfName=Any_WAN&sessionKey=";
            _acspost = _acspost + sessionKey;
           // Console.Write(_acspost);
            return (_acspost);
        }

        static void Main(string[] args)

        {
           var modem = new Browser();
            var modemURL = "http://192.168.20.1/tr69cfg.html";
                modem.Navigate(modemURL);
            //    Console.Write("get url");
            //    modem.BasicAuthenticationLogin("Broadband Router","admin","admin");
            modem.BasicAuthenticationLogin("192.168.20.1", "admin", "admin");


            modem.Navigate(modemURL);
           //     Console.Write(modem.CurrentHtml);
            var html = modem.CurrentHtml;

            string pattern = @"var sessionKey='([^']+)';";
            Match match = Regex.Match(html, pattern);
            string sessionKey = match.Groups[1].Value;
           // Console.Write(sessionKey);
            sessionKey = getSessionID(html);
           // Console.Write(sessionKey);
           // Console.Read();
            // create response

            // tr69cfg.cgi?tr69cEnable=1&tr69cInformEnable=1&tr69cInformInterval=86402&tr69cAcsURL=http://acs.netcommwireless.com/cpe.php&tr69cAcsUser=cpe&tr69cAcsPwd=cpe&tr69cConnReqUser=cpe&tr69cConnReqPwd=cpe&tr69cConnReqPort=30005&tr69cNoneConnReqAuth=0&tr69cDebugEnable=0&tr69cBoundIfName=Any_WAN&sessionKey=1045881970

            var getstringACS = ACSConfigString(sessionKey);
            modemURL = modemURL + getstringACS;
            modem.Navigate(modemURL);
        }
    }
}
