using CK.MQTT;
using System;
using System.Collections.Generic;

namespace ITI.SusanooQuest.MQTT
{

    /// <summary>
    /// Verify if a Client can connect at one MQTT Server
    /// </summary>
    internal class Certificate : IMqttAuthenticationProvider
    {
        readonly Dictionary<string, Dictionary<string, string>> _autentificate;
        // the key is client id, value contains  a dictionary with the client's user name and password
        readonly uint _maxClient;

        /// <summary>
        /// Initialize the certificate with a limited number connections on the server
        /// </summary>
        /// <param name="maxClient">Limit number of clients connected at the server</param>
        internal Certificate(uint maxClient)
        {
            _maxClient = maxClient;
            _autentificate = new Dictionary<string, Dictionary<string, string>>();
        }

        public bool Authenticate(string clientID, string username, string password)
        {
            if (string.IsNullOrEmpty(clientID)) throw new ArgumentException("Client ID can't be null or empty.", nameof(clientID));

            if (!_autentificate.ContainsKey(clientID))
            {
                if (_autentificate.Count >= 2) return false;
                else
                {
                    _autentificate.Add(
                        clientID,
                        new Dictionary<string, string>()
                        {
                            { "username", username },
                            { "password", password }
                        }
                    );
                    // if the client is not register on the Certificate then he is register on the autentificate list
                    return true;
                }
            }
            else
            {
                if (_autentificate[clientID]["username"] == username && _autentificate[clientID]["password"] == password) return true;
                // if the client was already connected to the server, then it will be authenticated
                else return false;
            }
        }

        internal void DestroyUser(string clientID)
        {
            if (string.IsNullOrEmpty(clientID)) throw new ArgumentException("Client ID can't be null or empty.", nameof(clientID));

            _autentificate.Remove(clientID);
        }
    }
}
