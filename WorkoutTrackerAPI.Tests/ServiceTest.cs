using NUnit.Framework;
using WorkoutTracker.Service.Controllers;

namespace WorkoutTracker.Tests
{
    [TestFixture]
    public class ServiceTest
    {
        [Test]
        [Category("ServiceTest")]
        public void TestMethod_LinkToServiceAssembly()
        {
            TrackingController _testController = new TrackingController();
            var result = _testController.Post(null);
            Assert.IsNotNull(result);
        }
    }
}
