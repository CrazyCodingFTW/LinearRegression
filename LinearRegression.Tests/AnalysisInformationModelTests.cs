using LinearRegression.Database.Model;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinearRegression.Tests
{
    [TestFixture]
    class AnalysisInformationModelTests
    {
        private AnalysisInformation analysisInfo;
        private DateTime currentDate;

        [SetUp]
        public void Init()
        {
            var now = DateTime.Now;

            this.currentDate = new DateTime(now.Year, now.Month, now.Day, now.Hour, now.Minute, now.Second);
            this.analysisInfo = new AnalysisInformation(currentDate, "test", "for testing");
        }

        [Test]
        public void AnalysisInformationConvertDateTimeToStringWorksProperly()
        {
            var expectedDateTimeString = this.currentDate.ToString("yyyy-MM-dd HH:mm:ss");
            var actualDateTimeString = analysisInfo.CreationDate;

            Assert.That(actualDateTimeString, Is.EqualTo(expectedDateTimeString));
        }

        [Test]
        public void AnalysisInformationGetDateTimeFromStringShouldReturnCorrectDateTime()
        {
            var actualDateTime = analysisInfo.GetDateTimeFromString();

            Assert.That(this.currentDate == actualDateTime);
        }
    }
}
