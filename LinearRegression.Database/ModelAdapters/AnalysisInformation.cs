using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LinearRegression.Database.Model;

namespace LinearRegression.Database.ModelAdapters
{
    public class AnalysisInformation : ModelAdapter<Model.AnalysisInformation>
    {
        private AnalysisData data;

        public AnalysisInformation(DateTime creationDate, string title, string description)
        {
            this.CreationDate = creationDate;
            this.Title = title;
            this.Descrioption = description;
        }

        /// <summary>
        /// Constructor for data mapping
        /// </summary>
        /// <param name="ai"></param>
        public AnalysisInformation(Model.AnalysisInformation ai) : this(ai.GetDateTimeFromString(), ai.Title, ai.Descrioption)
        {
            Entity = ai;
            this.Id = ai.Id;
        }

        internal override Model.AnalysisInformation Entity { get; set; }

        public  AnalysisData Data
        {
            get
            {
                if(this.data is null)
                    this.data = GetEntity<AnalysisData, Model.AnalysisData>(Entity.AnalysisDataId);

                return this.data;
            }

            set => this.data = value;
        }
        public DateTime CreationDate { get; set; }
        public string Title { get; set; }
        public string Descrioption { get; set; }

        protected override void OnSave()
        {
            if(Entity is null)
                this.Entity = new Model.AnalysisInformation(this.CreationDate, this.Title, this.Descrioption);

            else
            {
                Entity.ConvertDatetimeToString(this.CreationDate);
                Entity.Title = this.Title;
                Entity.Descrioption = this.Descrioption;
            }
        }

        /// <summary>
        /// Deletes the information and the data related with it
        /// </summary>
        protected override void OnDelete()
        {
            this.Data.Delete();
        }
    }
}
