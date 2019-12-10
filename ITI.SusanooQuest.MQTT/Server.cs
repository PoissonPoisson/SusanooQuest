using CK.MQTT;
using System;
using System.Collections.Generic;
using System.Net.NetworkInformation;

namespace ITI.SusanooQuest.MQTT
{
    public class Server : IDisposable
    {
        readonly IMqttServer _server;
        readonly MqttConfiguration _configuration;
        readonly Certificate _certificate;

        public Server(ushort port = 20202)
        {
            _certificate = new Certificate(2);

            _configuration = new MqttConfiguration()
            {
                Port = port,
                MaximumQualityOfService = MqttQualityOfService.ExactlyOnce
            };
            
            _server = MqttServer.Create(_configuration, authenticationProvider: _certificate);
            _server.Start();
        }

        public int Connections
        {
            get
            {
                try
                {
                    return _server.ActiveConnections;
                }
                catch (Exception e)
                {
                    Console.WriteLine("Server is down.");
                    return -1;
                }
            }
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
