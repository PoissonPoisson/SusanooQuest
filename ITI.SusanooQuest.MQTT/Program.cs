using System;
using System.Collections.Generic;
using CK.MQTT;
using System.Threading.Tasks;

namespace ITI.SusanooQuest.MQTT
{
    class Program
    {
    static void Main(string[] args)
        {
            switch (RequestMenu().ToUpper())
            {
                case "S":
                    RunServer();
                    break;
                case "C":
                    RunClient();
                    break;
                default:
                    Console.WriteLine("\nNo menu selected.");
                    break;
            }
        }

        static void PrintPortList(List<string[]> portList)
        {
            Console.WriteLine();
            int maxSize = 0;
            foreach (string[] data in portList)
            {
                if (data[0].Length > maxSize) maxSize = data[0].Length;
            }
            foreach (string[] data in portList)
            {
                string subString = "";
                for (int i = 0; i < maxSize - data[0].Length; i++) subString += " ";
                Console.WriteLine($"{data[0]}{subString} : {data[1]}");
            }
        }

        static string RequestMenu()
        {
            string request;
            while (true)
            {
                Console.WriteLine("What do you want to be ?\n S : Server\n C : Client\n");
                request = Console.ReadKey().KeyChar.ToString();
                if (request == "S" || request == "C") return request;
            }
        }

        static void RunServer()
        {
            ushort port = 20202;

            List<string[]> portList = Server.GetIPAddresses();
            PrintPortList(portList);

            using (Server server = new Server(port))
            {
                while (true)
                {
                    Console.WriteLine($"Number of connections : {server.Connections}");
                }
                //using (Client client = new Client(serverIP, port))
                //{
                //    while (!client.IsConnected) { }
                //    Console.WriteLine("Connected !");
                //    string topicName = "Public";
                //    client.Subcribe(topicName);
                //    client.Publish(topicName, "coucou");
                //}
            }
        }

        static void RunClient()
        {
            string serverIP = "";
            while (!Client.VerifyIfIPAddressIsValid(serverIP))
            {
                Console.Write("\nEnter an server IP address : ");
                serverIP = Console.ReadLine();
            }

            string message;
            string topic;
            using (Client client = new Client(serverIP, 20202))
            {
                Console.Write("\nEnter an topic name : ");
                topic = Console.ReadLine();
                client.Subcribe(topic);

                Console.Write("\nEnter your message : ");
                client.Publish(topic, Console.ReadLine());

                while (true)
                {
                    Console.WriteLine($"\n{client.GetMessage()}");
                }
            }
        }
    }
}
