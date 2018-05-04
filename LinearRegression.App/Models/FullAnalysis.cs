using LinearRegression.App.Contracts;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinearRegression.App.Models
{
    public class FullAnalysis<TAnalysisDataRow> : AnalysisMetadata, IFullAnalysis<TAnalysisDataRow> where TAnalysisDataRow : class, IAnalysisDataRow
    {
        /// <summary>
        /// Creates analysis model by giving the ability to be modified once. Gives default DateTime to the record which is now.
        /// </summary>
        /// <param name="title"></param>
        /// <param name="description"></param>
        /// <param name="xMeaning"></param>
        /// <param name="yMeaning"></param>
        /// <param name="data"></param>
        public FullAnalysis(string title, string description, string xMeaning, string yMeaning, IEnumerable<TAnalysisDataRow> data)
            : this(title, description, xMeaning, yMeaning, DateTime.Now, data) { }

        /// <summary>
        /// Creates analysis model by giving the ability to be modified once
        /// </summary>
        /// <param name="title"></param>
        /// <param name="description"></param>
        /// <param name="xMeaning"></param>
        /// <param name="yMeaning"></param>
        /// <param name="creationDate"></param>
        /// <param name="data"></param>
        public FullAnalysis(string title, string description, string xMeaning, string yMeaning, DateTime creationDate, IEnumerable<TAnalysisDataRow> data)
            : base(title, description, creationDate)
        {
            this.XMeaning = xMeaning;
            this.YMeaning = yMeaning;

            this.Data = new ObservableCollection<TAnalysisDataRow>(data);
        }

        public ObservableCollection<TAnalysisDataRow> Data { get; private set; }

        public string XMeaning { get; private set; }

        public string YMeaning { get; private set; }
    }
}
