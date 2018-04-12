using NUnit.Framework;
using System;

using LinearRegression.Database.Model;
using LinearRegression.Database.ModelContracts;
using Moq;
using System.Linq;

namespace LinearRegression.Tests
{
    [TestFixture]
    public class AnalysisDataModelTests
    {
        private IAnalysisInformation analysisInfoMock;

        private double[] demoX = new double[] { 1, 2, 3, 4, 5 };
        private double[] demoY = new double[] { 6, 7, 8, 9, 10 };

        [SetUp]
        public void Init()
        {
            var analysisInfoMock = new Mock<IAnalysisInformation>();
            analysisInfoMock.Setup(ai => ai.Id).Returns(1);

            this.analysisInfoMock = analysisInfoMock.Object;
        }

        [Test]
        public void AnalysisDataConvertDataToStringObjectShouldProvideStringInCorrectFormat()
        {
            var expectedStringX = string.Join("|", demoX);
            var expectedStringY = string.Join("|", demoY);

            var analysis = new AnalysisData("Test", demoX, "Test", demoY, this.analysisInfoMock);

            var actualStringX = analysis.XData;
            var actualStringY = analysis.YData;

            Assert.That(actualStringX, Is.EqualTo(expectedStringX));
            Assert.That(actualStringY, Is.EqualTo(expectedStringY));
        }

        [Test]
        public void AnalysisDataGetDataStringFromObjectShouldReturnArrayFromStringObject()
        {
            var analysis = new AnalysisData("Test", demoX, "Test", demoY, this.analysisInfoMock);

            var actualX = analysis.GetDataFromStringObject(DataType.X);
            var actualY = analysis.GetDataFromStringObject(DataType.Y);

            Assert.That(Enumerable.SequenceEqual(demoX, actualX));
            Assert.That(Enumerable.SequenceEqual(demoY, actualY));
        }
    }
}
