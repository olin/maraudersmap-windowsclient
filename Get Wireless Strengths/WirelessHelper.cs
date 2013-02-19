//Adapted from Managed Wifi example by Cory Dolphin
//Outputs Wireless Signal Strengths in JSON format, an array with many objects as described below:
//EXAMPLE: [{"SSID":"OLIN_GUEST","BSSID":"00:15:E8:E3:DF","RSSI":"-78"}]


using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Text;
using NativeWifi;

namespace MaraudersMap
{
    public class WirelessHelper
    {
        private WlanClient client;
        public WirelessHelper()
        {
            client = new WlanClient();
        }
        

        public Dictionary<string, List<SSIDEntry>> GetWirelessStrengths()
        {
            Dictionary<string, List<SSIDEntry>> resultDict = new Dictionary<string, List<SSIDEntry>>();
            foreach (WlanClient.WlanInterface wlanIface in client.Interfaces)
            {
                foreach (Wlan.WlanBssEntry network in wlanIface.GetNetworkBssList())
                {
                    int rss = network.rssi;
                    byte[] macAddrBytes = network.dot11Bssid;

                    string tMac = "";
                    for (int i = 0; i < macAddrBytes.Length; i++)
                    {
                        tMac += macAddrBytes[i].ToString("x2").PadLeft(2, '0').ToUpper();
                    }

                    string macAddress = "";
                    for (int i = 0; i < (tMac.Length / 2); i++)
                    {
                        macAddress += tMac.Substring(i * 2, 2);//note C# substring(i,j) goes from i to i+j
                        if (i != 5) { macAddress += ":"; }
                    }

                    string ssid = System.Text.ASCIIEncoding.ASCII.GetString(network.dot11Ssid.SSID).ToString().Replace(((char)0) + "", ""); //replace null chars
                    
                    if (!resultDict.ContainsKey(ssid))
                        resultDict[ssid] = new List<SSIDEntry>();
                    
                    resultDict[ssid].Add(new SSIDEntry(ssid, macAddress, rss.ToString()));
                }
            }

            return resultDict;
        }
        static void Main(string[] args)
        {
            WirelessHelper wclient = new WirelessHelper();
            Dictionary<string, List<SSIDEntry>> wirelessList = wclient.GetWirelessStrengths();
            Console.Write("[");
            foreach (List<SSIDEntry> ssids in wirelessList.Values)
            {
                foreach (SSIDEntry ssid in ssids)
                {
                    Console.Write(ssid.ToString());
                }
            }
            Console.Write("]");

        }
    }
    public class SSIDEntry
    {
        private string SSID, MACAddress, RSSI;
        public SSIDEntry(string SSID, string MACAddress, string RSSI)
        {
            this.SSID = SSID;
            this.MACAddress = MACAddress;
            this.RSSI = RSSI;
        }


        public new String ToString()
        {
            return String.Format("{'ssid':'{0}','mac':'{1}','rssi':'{2}'}", SSID,MACAddress,RSSI);
        }
    }

}