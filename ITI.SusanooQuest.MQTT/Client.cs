﻿using CK.MQTT;
using System;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;

namespace ITI.SusanooQuest.MQTT
{
    public class Client : IDisposable
    {
        readonly MqttConfiguration _configuration;
        readonly IMqttClient _client;
        readonly MqttClientCredentials _credential;

        public Client(string serverIP, ushort port)
        {
            if (string.IsNullOrEmpty(serverIP)) throw new ArgumentException("Server IP address is null or empty", nameof(serverIP));
            if (!VerifyIfIPAddressIsValid(serverIP)) throw new ArgumentException("Server IP address is not valid", nameof(serverIP));

            _configuration = new MqttConfiguration()
            {
                MaximumQualityOfService = MqttQualityOfService.ExactlyOnce,
                Port = port,
                WaitTimeoutSecs = 5
            };

            _client = MqttClient.CreateAsync(serverIP, _configuration).Result;

            _credential = new MqttClientCredentials(
                Guid.NewGuid().ToString().Replace("-", ""),
                Guid.NewGuid().ToString(),
                Guid.NewGuid().ToString()
            );

            SessionState s = _client.ConnectAsync(_credential, cleanSession: true).Result;
        }

        public void Subcribe(string topicName)
        {
            if (string.IsNullOrEmpty(topicName)) throw new ArgumentException("Topic name is null or empty", nameof(topicName));
            if (topicName.Trim() == string.Empty) throw new ArgumentException("Topic name can't contain just space", nameof(topicName));

            _client.SubscribeAsync(topicName, _configuration.MaximumQualityOfService);
        }

        public void Publish(string topicName, string message)
        {
            if (string.IsNullOrEmpty(topicName)) throw new ArgumentException("Topic name is null or empty", nameof(topicName));
            if (topicName.Trim() == string.Empty) throw new ArgumentException("Topic name can't contain just space", nameof(topicName));
            if (string.IsNullOrEmpty(message)) throw new ArgumentException("Message is null or empty", nameof(message));

            _client.PublishAsync(
                new MqttApplicationMessage(topicName, Encoding.ASCII.GetBytes(message)),
                _configuration.MaximumQualityOfService
            );

            //byte[] msg = Encoding.ASCII.GetBytes("coucou");
            //var m = new MqttApplicationMessage("coucou", msg);

            //IObservable<MqttApplicationMessage> msg = _client.MessageStream;
            //IDisposable  msg.Subscribe();

            //MqttClientCredentials a = new MqttClientCredentials("myTopic", "username", "password");
            // test credentials
        }

        public string GetMessage(int a)
        {
            IObservable<MqttApplicationMessage> msg =_client.MessageStream;
            IDisposable m = msg.Subscribe();
            return m.ToString();
        }

        public void GetMessage()
        {
            _client.MessageStream.Subscribe(msg => Console.WriteLine($"Message recevied in the topic : {msg.Topic}\n  -> {Encoding.ASCII.GetString(msg.Payload)}"));
        }

        public bool IsConnected => _client.IsConnected;

        public string ID => _client.Id;

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
