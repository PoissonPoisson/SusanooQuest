using System;
using System.Net;
using System.Threading;
using System.Text.RegularExpressions;
using MQTTnet;
using MQTTnet.Client;
using MQTTnet.Client.Options;
using MQTTnet.Client.Subscribing;
using MQTTnet.Client.Connecting;
using MQTTnet.Client.Disconnecting;
using System.Threading.Tasks;

namespace ITI.SusanooQuest.MQTT
{
    /// <summary>
    /// class Client MQTT
    /// </summary>
    public class Client
    {
        readonly IMqttClient _mqttClient;
        readonly IMqttClientOptions _options;
        string _topic;

        /// <summary>
        /// Create a MQTT client
        /// </summary>
        /// <param name="serverIP">IP in IPV4</param>
        /// <param name="port">Server port</param>
        public Client(string serverIP, ushort port)
        {
            if (string.IsNullOrEmpty(serverIP)) throw new ArgumentException("Server IP address is null or empty", nameof(serverIP));
            if (!VerifyIfIPAddressIsValid(serverIP)) throw new ArgumentException("Server IP address is not valid", nameof(serverIP));

            _mqttClient = new MqttFactory().CreateMqttClient();

            _options = new MqttClientOptionsBuilder()
                .WithClientId("J1")
                .WithTcpServer(serverIP, port)
                .WithTls()
                .WithTopicAliasMaximum(2)
                .Build();
        }

        public void ConnectClient()
        {
            //MqttClientAuthenticateResult result = await _mqttClient.ConnectAsync(_options, CancellationToken.None);

            Task<MqttClientAuthenticateResult> result = _mqttClient.ConnectAsync(_options, CancellationToken.None);
        }

        internal void Subscribing()
        {
            //MqttClientSubscribeOptions subcribingOption = new MqttClientSubscribeOptions();

            Task<MqttClientSubscribeResult> result = _mqttClient.SubscribeAsync("Public");
        }

        public async void DisconectClient()
        {
            await _mqttClient.DisconnectAsync();
        }

        async void SendEvent()
        {
            throw new NotImplementedException();
        }

        static bool VerifyIfIPAddressIsValid(string ipAddress)
        {
            if (Regex.IsMatch(ipAddress, @"\b\d{1,3}\.\d{1,3}\.\d{1,3}\.\d{1,3}\b"))
                return IPAddress.TryParse(ipAddress, out IPAddress address);
            else return false;
        }
    }
}
