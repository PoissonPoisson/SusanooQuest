using CK.MQTT;
using System;
using System.Collections.Generic;

namespace ITI.SusanooQuest.MQTT
{
    internal class Certificate : IMqttAuthenticationProvider
    {
        readonly Dictionary<string, Dictionary<string, string>> _autentificate;
        readonly uint _maxClient;

        internal Certificate(uint maxClient)
        {
            _maxClient = maxClient;
            _autentificate = new Dictionary<string, Dictionary<string, string>>();
        }

        public bool Authenticate(string clientID, string username, string password)
        {
            if (string.IsNullOrEmpty(clientID)) throw new ArgumentException("Client ID can't be null or empty.", nameof(clientID));

            if (_autentificate.Count < 2 && !_autentificate.ContainsKey(clientID))
            {
                _autentificate.Add(
                    clientID,
                    new Dictionary<string, string>()
                    {
                        { "username", username },
                        { "password", password }
                    }
                );
                return true;
            }
            else return false;
        }

        internal void DestroyUser(string clientID)
        {
            if (string.IsNullOrEmpty(clientID)) throw new ArgumentException("Client ID can't be null or empty.", nameof(clientID));

            _autentificate.Remove(clientID);
        }
    }
}
