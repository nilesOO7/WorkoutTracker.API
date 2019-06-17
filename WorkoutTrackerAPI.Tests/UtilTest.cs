using System;
using NUnit.Framework;
using WorkoutTracker.Data.Common;

namespace WorkoutTracker.Tests
{
    [TestFixture]
    public class UtilTest
    {
        [Test]
        [Category("UtilTest")]
        public void TestMethod_GetWeekofMonth()
        {
            Assert.AreEqual(1, DateTime.Parse("2018-04-04").GetWeekOfMonth());
            Assert.AreEqual(3, DateTime.Parse("2018-04-16").GetWeekOfMonth());
            Assert.AreEqual(1, DateTime.Parse("2018-06-01").GetWeekOfMonth());
            Assert.AreEqual(5, DateTime.Parse("2018-04-30").GetWeekOfMonth());
        }

        [Test]
        [Category("UtilTest")]
        public void TestMethod_GetWeekofMonthPartial()
        {
            Assert.AreEqual(3, DateTime.Parse("2018-04-20").GetWeekOfMonthPartial());
            Assert.AreEqual(5, DateTime.Parse("2018-03-31").GetWeekOfMonthPartial());
            Assert.AreEqual(1, DateTime.Parse("2018-04-01").GetWeekOfMonthPartial());
            Assert.AreEqual(2, DateTime.Parse("2018-04-08").GetWeekOfMonthPartial());
            Assert.AreEqual(1, DateTime.Parse("2018-04-04").GetWeekOfMonthPartial());
            Assert.AreEqual(3, DateTime.Parse("2018-04-16").GetWeekOfMonthPartial());
            Assert.AreEqual(1, DateTime.Parse("2018-06-01").GetWeekOfMonthPartial());
            Assert.AreEqual(5, DateTime.Parse("2018-04-30").GetWeekOfMonthPartial());
            Assert.AreEqual(1, DateTime.Parse("2018-09-01").GetWeekOfMonthPartial());
            Assert.AreEqual(5, DateTime.Parse("2018-08-31").GetWeekOfMonthPartial());
        }
    }
}
