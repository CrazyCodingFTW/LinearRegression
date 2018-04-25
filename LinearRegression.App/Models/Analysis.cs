using LinearRegression.App.Contracts;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinearRegression.App.Models
{
    public class Analysis : IAnalysisModel
    {
        /// <summary>
        /// Creates analysis model by giving the ability to be modified once. Gives default DateTime to the record which is now.
        /// </summary>
        /// <param name="title"></param>
        /// <param name="description"></param>
        /// <param name="xMeaning"></param>
        /// <param name="yMeaning"></param>
        /// <param name="data"></param>
        public Analysis(string title, string description, string xMeaning, string yMeaning, IEnumerable<IAnalysisDataRow> data) :
            this(title, description, xMeaning, yMeaning, DateTime.Now, data)
        {

        }

        /// <summary>
        /// Creates analysis model by giving the ability to be modified once
        /// </summary>
        /// <param name="title"></param>
        /// <param name="description"></param>
        /// <param name="xMeaning"></param>
        /// <param name="yMeaning"></param>
        /// <param name="creationDate"></param>
        /// <param name="data"></param>
        public Analysis(string title, string description, string xMeaning, string yMeaning, DateTime creationDate, IEnumerable<IAnalysisDataRow> data)
        {
            this.Title = title;
            this.Description = description;
            this.XMeaning = xMeaning;
            this.YMeaning = yMeaning;
            this.CreationDate = creationDate;
        }

        public string Title { get; private set; }

        public string Description { get; private set; }

        public ObservableCollection<IAnalysisDataRow> Data { get; private set; }

        public DateTime CreationDate { get; private set; }

        public string XMeaning { get; private set; }

        public string YMeaning { get; private set; }
    }
}
