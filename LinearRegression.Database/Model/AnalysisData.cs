using LinearRegression.Database.ModelContracts;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinearRegression.Database.Model
{
    public class AnalysisData : IAnlysisData
    {
        public AnalysisData() { }

        public AnalysisData(string xMeaning, IEnumerable<double> xData, string yMeaning, IEnumerable<double> yData, IAnalysisInformation analysisInformation)
        {
            this.XMeaning = xMeaning;
            ConvertDataToStringObject(xData, DataType.X);
            this.YMeaning = yMeaning;
            ConvertDataToStringObject(yData, DataType.Y);

            if (analysisInformation.Id <= 0)
                throw new ArgumentException("Cannot link unsaved AnalysisInformation object.");

            this.AnalysisInformationId = analysisInformation.Id;
        }


        public long Id { get; set; }
        public long AnalysisInformationId { get; set; }
        public string XData { get; set; }
        public string XMeaning { get; set; }
        public string YData { get; set; }
        public string YMeaning { get; set; }
        public long AnalysisCalculationsId { get; set; }

        public void ConvertDataToStringObject(IEnumerable<double> data, DataType dataType)
        {
            var stringData = string.Join("|", data);

            switch (dataType)
            {
                case DataType.X:
                    this.XData = stringData;
                    break;

                case DataType.Y:
                    this.YData = stringData;
                    break;
            }
        }

        public IEnumerable<double> GetDataFromStringObject(DataType dataType)
        {
            switch (dataType)
            {
                case DataType.X:
                    return this.XData.Split('|').Select(double.Parse);

                case DataType.Y:
                    return this.YData.Split('|').Select(double.Parse);
            }

            return null;
        }
    }
}
