using System;

namespace ITI.SusanooQuest.MQTT
{
    class Program
    {
        static void Main(string[] args)
        {
            ushort port = 1884;

            Server server = new Server(port);
            server.Start();

            Client client = new Client("127.0.0.1", port);
            client.ConnectClient();

            Console.WriteLine("Press any key to exit.");
            Console.ReadLine();

            client.DisconectClient();
            server.Stop();
        }
    }
}
