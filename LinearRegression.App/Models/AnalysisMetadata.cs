using LinearRegression.App.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinearRegression.App.Models
{
    public class AnalysisMetadata : IAnalysisMetadata
    {
        /// <summary>
        /// Creates metadata object with a given date
        /// </summary>
        /// <param name="title"></param>
        /// <param name="description"></param>
        /// <param name="creationDate"></param>
        public AnalysisMetadata(string title, string description, DateTime creationDate)
        {
            this.Title = title;
            this.Description = description;
            this.CreationDate = creationDate;
        }

        /// <summary>
        /// Creates metadata object with current date
        /// </summary>
        /// <param name="title"></param>
        /// <param name="description"></param>
        public AnalysisMetadata(string title, string description):this(title, description, DateTime.Now)
        {

        }

        public string Title { get; protected set; }

        public string Description { get; protected set; }

        public DateTime CreationDate { get; protected set; }
    }
}
