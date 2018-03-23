using System;
using System.Collections.Generic;

namespace LinearRegression.Contracts.ModelContracts
{
    public enum DataType { X, Y}
    public interface IAnlysis
    {
        long Id { get; set; }

        /// <summary>
        /// Represents the crateion date in string. Use ConvertDataToStringObject to get the datetime
        /// </summary>
        string CreationDate { get; set; }
        string Title { get; set; }
        string Descrioption { get; set; }

        /// <summary>
        /// Represents an array with all the X values. Use ConvertDataToStringObject to get the array
        /// </summary>
        string XData { get; set; }
        string XMeaning { get; set; }

        /// <summary>
        /// Represents an array with all the Y values. Use ConvertDataToStringObject to get the array
        /// </summary>
        string YData { get; set; }
        string YMeaning { get; set; }

        /// <summary>
        /// Gets the data for either X or Y and converts it into a suitable for the database type
        /// </summary>
        /// <param name="data">The array of the X or Y values</param>
        /// <param name="dataType">Specify to which parameter does the data belong</param>
        void ConvertDataToStringObject(IEnumerable<double> data, DataType dataType);

        /// <summary>
        /// Get the data, represented in enumerable state
        /// </summary>
        /// <param name="dataType">Chose which data do you want either X or Y</param>
        /// <returns></returns>
        IEnumerable<double> GetDataFromStringObject(DataType dataType);

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
