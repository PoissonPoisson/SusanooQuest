using System;
using System.Net;
using System.Text.RegularExpressions;
using NUnit.Framework;
using ITI.SusanooQuest.MQTT;

namespace ITI.SusanooQuest.Tests
{
    [TestFixture]
    public class MQTT
    {
        [TestCase("0.0.0.0")]
        [TestCase("127.0.0.1")]
        [TestCase("127.127.127.127")]
        [TestCase("255.255.255.255")]
        public void test_valid_IP_adress(string ipAddress)
        {
            Assert.That(Client.VerifyIfIPAddressIsValid(ipAddress));
        }

        [TestCase("")]
        [TestCase("coucou")]
        [TestCase("127")]
        [TestCase("127.127")]
        [TestCase("127.127.127")]
        [TestCase("127.127.127.127.127")]
        [TestCase("-1.-1.-1.-1")]
        [TestCase("300.300.300.300")]
        [TestCase("1000.1000.1000.1000")]
        public void test_invalid_IP_adress(string ipAddress)
        {
            Assert.Throws<ArgumentException>(() => new Client(ipAddress, 20202));
        }

        [Test]
        public void test_if_client_can_to_connect_at_server()
        {
            ushort port = 20202;
            string serverIP = "127.0.0.1";

            using (Server server = new Server(port))
            {
                using (Client client = new Client(serverIP, port))
                {
                    Assert.That(client.IsConnected, Is.True);
                    client.Disconnect();
                }
            }
        }
    }
}
