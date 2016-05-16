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

        static void vlanConfiguration()
        {
            var modem = new Browser();

            var modemURL = "http://192.168.20.1/qinetsetup.html";
            modem.Navigate(modemURL);
            //    Console.Write("get url");
           //    modem.BasicAuthenticationLogin("Broadband Router","admin","admin");
            modem.BasicAuthenticationLogin("192.168.20.1", "admin", "admin");


            modem.Navigate(modemURL);
            //     Console.Write(modem.CurrentHtml);
            var html = modem.CurrentHtml;
            string sessionKey = getSessionID(html);
            // post wlan type
            modemURL = "http://192.168.20.1/qvdslwanmode.cgi?wanType=2&sessionKey=" + sessionKey;
            modem.Navigate(modemURL);
            html = modem.CurrentHtml;
            sessionKey = getSessionID(html);
            modemURL = "http://192.168.20.1/qvdslppp.cgi?ntwkPrtcl=0&enblOnDemand=0&pppTimeOut=0&enblv4=1&useStaticIpAddress=0&pppIpExtension=0&enblFirewall=1&enblNat=1&enblIgmp=1&keepalive=1&keepalivetime=5&alivemaxfail=30&enVlanMux=1&vlanMuxId=69&vlanMuxPr=0&enblPppDebug=0&maxMtu=1492&keepalive=0&enblv6=0&pppAuthErrorRetry=0&pppAuthMethod=0&sessionKey=" + sessionKey;
            modem.Navigate(modemURL);
            html = modem.CurrentHtml;
            sessionKey = getSessionID(html);
            modemURL = "http://192.168.20.1/qsetup.cmd?pppUserName=craignz&pppPassword=craigpassword&portId=0&ptmPriorityNorm=1&ptmPriorityHigh=1&connMode=1&burstsize=3000&enblQos=1&grpPrec=8&grpAlg=WRR&grpWght=1&prec=8&alg=WRR&wght=1&sessionKey=" + sessionKey;
            modem.Navigate(modemURL);
           

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
            vlanConfiguration();
            // need to do 4 posts to configure vlan
        }
    }
}
