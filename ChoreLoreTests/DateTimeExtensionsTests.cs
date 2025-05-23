using ChoreLore.Extensions;

namespace ChoreLoreTests
{
    public class DateTimeExtensionsTests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void StartOfWeekTest()
        {
            var testDate = new DateTime(2025, 5, 23);
            var startOfWeek = testDate.StartOfWeek();

            Assert.That(startOfWeek, Is.EqualTo(new DateTime(2025, 5, 18)));
        }

        [Test]
        public void EndOfWeekTest()
        {
            var testDate = new DateTime(2025, 5, 23);
            var startOfWeek = testDate.EndOfWeek();

            Assert.That(startOfWeek, Is.EqualTo(new DateTime(2025, 5, 24)));
        }
    }
}