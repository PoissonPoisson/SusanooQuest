using System;
using System.Collections.Generic;
using CK.MQTT;
using System.Net.NetworkInformation;

namespace ITI.SusanooQuest.MQTT
{
    public class Server : IDisposable
    {
        readonly IMqttServer _server;
        readonly MqttConfiguration _configuration;

        public Server(ushort port)
        {
            _configuration = new MqttConfiguration()
            {
                Port = port,
                MaximumQualityOfService = MqttQualityOfService.ExactlyOnce
            };
            
            _server = MqttServer.Create(_configuration, null, null);
            _server.Start();
            
            //MqttClientCredentials a = new MqttClientCredentials("myTopic", "username", "password");
            // test credentials
        }

        public int Connections
        {
            get { return _server.ActiveConnections; }
        }

        public void Dispose()
        {
            _server.Stop();
            _server.Dispose();
        }

        public static List<string[]> GetIPAddresses()
        {
            List<string[]> portsData = new List<string[]>();
            foreach (NetworkInterface ni in NetworkInterface.GetAllNetworkInterfaces())
            {
                foreach (UnicastIPAddressInformation ip in ni.GetIPProperties().UnicastAddresses)
                {
                    if (ip.Address.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)  
                    {
                        //Console.WriteLine($"{ip.Address.ToString()} : {ni.Name}");
                        portsData.Add(new string[2] { ni.Name, ip.Address.ToString() });
                    }
                }
            }
            return portsData;
        }
    }
}
