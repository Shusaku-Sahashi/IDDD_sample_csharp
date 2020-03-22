using NUnit.Framework;

namespace IDDDCommon.Test.Standard
{
    [TestFixture]
    public class IntegerExtensionFixture
    {
        [Test]
        public void CanParse()
        {
            var expected = "100000";
            var actual = expected.TryParseDefault(0);
            Assert.That(actual, Is.EqualTo(int.Parse(expected)));
        }
        
        [Test]
        public void CanGetDefaultWhenRiseException()
        {
            var expected = "aaa";
            var actual = expected.TryParseDefault(0);
            Assert.That(actual, Is.EqualTo(0));
        }
    }
}