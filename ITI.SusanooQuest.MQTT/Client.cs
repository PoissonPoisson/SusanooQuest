using System;
using CK.MQTT;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;

namespace ITI.SusanooQuest.MQTT
{
    public class Client : IDisposable
    {
        readonly IMqttClient _client;
        readonly MqttClientCredentials _credential;
        string _topic;

        public Client(string serverIP, ushort port)
        {
            if (string.IsNullOrEmpty(serverIP)) throw new ArgumentException("Server IP address is null or empty", nameof(serverIP));
            if (!VerifyIfIPAddressIsValid(serverIP)) throw new ArgumentException("Server IP address is not valid", nameof(serverIP));

            _client = MqttClient.CreateAsync(serverIP, port).Result;
            _credential = new MqttClientCredentials(Guid.NewGuid().ToString().Replace("-", ""), Guid.NewGuid().ToString(), Guid.NewGuid().ToString());
            SessionState s = _client.ConnectAsync(_credential, null, true).Result;
        }

        public void Subcribe(string topicName)
        {
            if (string.IsNullOrEmpty(topicName)) throw new ArgumentException("Topic name is null or empty", nameof(topicName));
            if (topicName.Trim() == string.Empty) throw new ArgumentException("Topic name can't contain just space", nameof(topicName));

            _client.SubscribeAsync(topicName, MqttQualityOfService.ExactlyOnce);
        }

        public void Publish(string topicName, string message)
        {
            if (string.IsNullOrEmpty(topicName)) throw new ArgumentException("Topic name is null or empty", nameof(topicName));
            if (topicName.Trim() == string.Empty) throw new ArgumentException("Topic name can't contain just space", nameof(topicName));
            if (string.IsNullOrEmpty(message)) throw new ArgumentException("Message is null or empty", nameof(message));

            _client.PublishAsync(
                new MqttApplicationMessage(topicName, Encoding.ASCII.GetBytes(message)),
                MqttQualityOfService.ExactlyOnce
            );

            //byte[] msg = Encoding.ASCII.GetBytes("coucou");
            //var m = new MqttApplicationMessage("coucou", msg);

            //IObservable<MqttApplicationMessage> msg = _client.MessageStream;
            //IDisposable  msg.Subscribe();
        }

        public string GetMessage()
        {
            IObservable<MqttApplicationMessage> msg =_client.MessageStream;
            IDisposable m = msg.Subscribe();
            return m.ToString();
        }

        public bool IsConnected => _client.IsConnected;

        public void Disconnect() 
        {
            _client.DisconnectAsync();
        }

        public void Dispose()
        {
            Disconnect();
            _client.Dispose();
        }

        public static bool VerifyIfIPAddressIsValid(string ipAddress)
        {
            if (Regex.IsMatch(ipAddress, @"\b\d{1,3}\.\d{1,3}\.\d{1,3}\.\d{1,3}\b"))
                return IPAddress.TryParse(ipAddress, out IPAddress address);
            else return false;
        }
    }
}
