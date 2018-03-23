using LinearRegression.Contracts.ModelContracts;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinearRegression.Database.Model
{
    class Analysis : IAnlysis
    {
        public Analysis()
        {

        }

        public Analysis(string title, string description, DateTime creationDate, string XMeaning, IEnumerable<double> XData, string YMeaning, IEnumerable<double> YData)
        {
            this.Title = title;
            this.Descrioption = description;
            ConvertDatetimeToString(creationDate);
            this.XMeaning = XMeaning;
            ConvertDataToStringObject(XData, DataType.X);
            this.YMeaning = YMeaning;
            ConvertDataToStringObject(YData, DataType.Y);
        }


        public long Id { get; set; }

        public string CreationDate { get; set; }
        public string Title { get; set; }
        public string Descrioption { get; set; }
        public string XData { get; set; }
        public string XMeaning { get; set; }
        public string YData { get; set; }
        public string YMeaning { get; set; }

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

        public void ConvertDatetimeToString(DateTime date)
        {
            this.CreationDate = date.ToString("yyyy-MM-dd HH:mm:ss");
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

        public DateTime GetDateTimeFromString()
        {
            var date = DateTime.ParseExact(this.CreationDate, "yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture);
            return date;
        }
    }
}
