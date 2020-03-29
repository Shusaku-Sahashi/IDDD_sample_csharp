using IDDDCommon.Port.Adapter.Messaging.Rabbitmq;
using NUnit.Framework;

namespace IDDDCommon.Test.Port.Adapter.Message.Rabbitmq
{
    [TestFixture]
    public class ConnectionSettingFixture
    {
        [Test]
        public void CanFetchSetting()
        {
            var config = ConnectionSettings.Instance();
            Assert.That(config.HostName, Is.EqualTo("localhost"));
        }
    }
}