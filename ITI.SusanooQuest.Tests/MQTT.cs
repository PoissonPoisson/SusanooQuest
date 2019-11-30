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
            Assert.That(new Client(ipAddress, 1884), Is.Not.Null);
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
            Assert.Throws<ArgumentException>(() => new Client(ipAddress, 1884));
        }
    }
}
