using LinearRegression.App.Contracts;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinearRegression.App.Models
{
    public class FullAnalysis : AnalysisMetadata, IFullAnalysis
    {
        /// <summary>
        /// Creates analysis model by giving the ability to be modified once. Gives default DateTime to the record which is now.
        /// </summary>
        /// <param name="title"></param>
        /// <param name="description"></param>
        /// <param name="xMeaning"></param>
        /// <param name="yMeaning"></param>
        /// <param name="data"></param>
        public FullAnalysis(string title, string description, string xMeaning, string yMeaning, IEnumerable<IAnalysisDataRow> data)
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
        public FullAnalysis(string title, string description, string xMeaning, string yMeaning, DateTime creationDate, IEnumerable<IAnalysisDataRow> data) 
            : base(title, description, creationDate)
        {
            this.XMeaning = xMeaning;
            this.YMeaning = yMeaning;

            this.Data = new ObservableCollection<IAnalysisDataRow>(data);
        }

        public ObservableCollection<IAnalysisDataRow> Data { get; private set; }

        public string XMeaning { get; private set; }

        public string YMeaning { get; private set; }
    }
}
