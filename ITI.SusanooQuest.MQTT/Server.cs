using CK.MQTT;
using System;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using System.Text;

namespace ITI.SusanooQuest.MQTT
{
    public class Server : IDisposable
    {
        readonly IMqttServer _server;
        readonly MqttConfiguration _configuration;
        readonly Certificate _certificate;

        public Server(ushort port = 20202)
        {
            _certificate = new Certificate(3);

            _configuration = new MqttConfiguration()
            {
                Port = port,
                MaximumQualityOfService = MqttQualityOfService.ExactlyOnce
            };
            
            _server = MqttServer.Create(_configuration, authenticationProvider: _certificate);

            _server.ClientConnected += (a, e) =>
            {
                Console.WriteLine("1");
                if (Connections == 2) SendTopicsName();
            };

            _server.Start();
        }

        public int Connections
        {
            get
            {
                try { return _server.ActiveConnections; }
                catch (Exception e)
                {
                    Console.WriteLine($"Error : {e.GetType()}\nMessage : {e.Message}");
                    return -1;
                }
            }
        }

        void SendTopicsName()
        {
            Console.WriteLine("2");
            using (IMqttClient client = MqttClient.CreateAsync("127.0.0.1", _configuration).Result)
            {
                SessionState session = client.ConnectAsync(
                    new MqttClientCredentials(
                        Guid.NewGuid().ToString().Replace("-", ""),
                        Guid.NewGuid().ToString(),
                        Guid.NewGuid().ToString()
                    ),
                    cleanSession: true
                ).Result;
                Console.WriteLine($"My ID is : {client.Id}");

                List<string> clientsID = new List<string>();
                foreach (string clientID in _server.ActiveClients)
                {
                    Console.WriteLine(clientID);
                    clientsID.Add(clientID);
                }
                clientsID.Remove(client.Id);

                for (ushort i = 0; i < 2; i++)
                {
                    client.SubscribeAsync(clientsID[i], MqttQualityOfService.ExactlyOnce);
                    client.PublishAsync(
                        new MqttApplicationMessage(
                            clientsID[i],
                            Encoding.ASCII.GetBytes(clientsID[(i + 1) % 2])
                        ),
                        MqttQualityOfService.ExactlyOnce
                    );
                    client.UnsubscribeAsync(clientsID[i]);
                }
                client.DisconnectAsync();
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
