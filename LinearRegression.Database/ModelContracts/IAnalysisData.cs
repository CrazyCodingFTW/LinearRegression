using System;
using System.Collections.Generic;

namespace LinearRegression.Database.ModelContracts
{
    /// <summary>
    /// Use this to poin which data is wanted
    /// </summary>
    public enum DataType { X, Y }
    public interface IAnlysisData : IDBEntity
    {
        long AnalysisInformationId { get; set; }

        long AnalysisCalculationsId { get; set; }

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
    }
}
