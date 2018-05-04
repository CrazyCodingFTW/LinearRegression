using System;
using System.Collections.Generic;
using System.Text;

namespace LinearRegression.Database.ModelContracts
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
        string CommentIDs { get; set; }

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

        /// <summary>
        /// Splits the comments ids into convenient to use array
        /// </summary>
        /// <returns></returns>
        long[] GetCommentIds();

        /// <summary>
        /// Gets the Id of the comment and adds it to the comment ids string
        /// </summary>
        /// <param name="comment"></param>
        void AddComment(IComment comment);

        /// <summary>
        /// Removes the comment with the given id from the comments string
        /// </summary>
        /// <param name="comment"></param>
        void RemoveComment(IComment comment);
    }
}
