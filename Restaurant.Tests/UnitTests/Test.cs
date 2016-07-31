using FluentAssertions;
using NUnit.Framework;

namespace Restaurant.Tests.UnitTests
{
    [TestFixture]
    class Test
    {
        private int x;
        private int y;

        [SetUp]
        public void TestFixtureSetUp()
        {
            x = 5;
            y = 5;
        }

        [Test]
        public void testing_something()
        {
            x = 4;
            y = 4;
            x.Should().Be(y);
        }

        [Test]
        public void testing_something2()
        {
            x.Should().Be(5);
            y.Should().Be(5);
        }
        
        [TestFixtureTearDown]
        public void TestFixtureTearDown()
        {

        }
    }
}
