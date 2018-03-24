using System;
using System.Collections.Generic;
using System.Text;

namespace LinearRegression.Contracts.ModelContracts
{
    public interface IAnalysisInformation : IDBEntity
    {
        long AnalysisDataId { get; set; }

        /// <summary>
        /// Represents the crateion date in string. Use ConvertDataToStringObject to get the datetime
        /// </summary>
        string CreationDate { get; set; }
        string Title { get; set; }
        string Descrioption { get; set; }


        /// <summary>
        /// Sets the date value to the database string CreationDate
        /// </summary>
        /// <param name="date"></param>
        void ConvertDatetimeToString(DateTime date);

        /// <summary>
        /// Gets the date from the CreationDate and converts it into DateTime
        /// </summary>
        /// <returns></returns>
        DateTime GetDateTimeFromString();
    }
}
