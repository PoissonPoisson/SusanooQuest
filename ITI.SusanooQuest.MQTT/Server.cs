using System;
using MQTTnet;
using MQTTnet.Server;

namespace ITI.SusanooQuest.MQTT
{
    public class Server
    {
        readonly IMqttServer _mqttServer;
        readonly MqttServerOptionsBuilder _optionsBuilder;
        readonly string _userName1;
        readonly string _userName2;
        readonly string _password;

        public Server(ushort port)
        {
            _optionsBuilder = new MqttServerOptionsBuilder()
                .WithDefaultEndpointPort(port);
            _mqttServer = new MqttFactory().CreateMqttServer();

            _password = Guid.NewGuid().ToString();
        }

        public async void Start()
        {
            await _mqttServer.StartAsync(_optionsBuilder.Build());
        }

        public async void Stop()
        {
            await _mqttServer.StopAsync();
        }

        public void WhoIsConnected()
        {
            Console.WriteLine(_mqttServer.ClientConnectedHandler.ToString());
        }
    }
}
