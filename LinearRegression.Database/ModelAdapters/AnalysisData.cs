using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LinearRegression.Database.ModelContracts;
using LinearRegression.Database.Model;

namespace LinearRegression.Database.ModelAdapters
{
    public class AnalysisData : ModelAdapter<Model.AnalysisData>
    {
        private AnalysisInformation analysisInformation;

        public AnalysisData(string xMeaning, IEnumerable<double> xData, string yMeaning, IEnumerable<double> yData, AnalysisInformation analysisInformation, IModelController<LinearRegressionDbContext> controller) : base(controller)
        {
            this.XMeaning = xMeaning;
            this.XData = xData;
            this.YMeaning = yMeaning;
            this.YData = yData;

            this.AnalysisInformation = analysisInformation;
        }

        /// <summary>
        /// Constructor for data mapping
        /// </summary>
        /// <param name="ad"></param>
        public AnalysisData(Model.AnalysisData ad, IModelController<LinearRegressionDbContext> controller) : base(ad, controller)
        {
            this.XMeaning = ad.XMeaning;
            this.XData = ad.GetDataFromStringObject(DataType.X);
            this.YMeaning = ad.YMeaning;
            this.YData = ad.GetDataFromStringObject(DataType.Y);
        }

        public AnalysisInformation AnalysisInformation
        {
            get
            {
                if (this.analysisInformation is null)
                    this.analysisInformation = Controller.GetEntityById<AnalysisInformation>(this.Entity.AnalysisInformationId);


                return this.analysisInformation;
            }

            set => this.analysisInformation = value;
        }
        public IEnumerable<double> XData { get; set; }
        public string XMeaning { get; set; }
        public IEnumerable<double> YData { get; set; }
        public string YMeaning { get; set; }

        /// <summary>
        /// The item cannot be deleted directly. Use the instance of AnalysisInformation
        /// </summary>
        public override void Delete()
        {
            throw new InvalidOperationException("The action cannot be done directly through this object. Please call Delete from the related AnalysisInformation instance!");
        }

        public override void Delete(LinearRegressionDbContext db)
        {
            this.Delete();
        }

        public override void Save(LinearRegressionDbContext db)
        {
            if (this.AnalysisInformation is null || this.AnalysisInformation.Id <= 0)
                throw new ArgumentException("Cannot set unsaved analysis information to the analysis data");

            // If the AnalysisData adapter is newly created, it shouldn't be related with any entity
            if (Entity is null)
                this.Entity = new Model.AnalysisData(this.XMeaning, this.XData, this.YMeaning, this.YData, this.AnalysisInformation.Entity);

            //If there is entity related with the AnalysisData instance, it will get updated
            else
            {
                Entity.XMeaning = this.XMeaning;
                Entity.ConvertDataToStringObject(XData, DataType.X);
                Entity.YMeaning = this.YMeaning;
                Entity.ConvertDataToStringObject(YData, DataType.Y);
            }

            if (db.AnalysisDataSet.Any(d => d.Id == Entity.Id))
                db.AnalysisDataSet.Update(this.Entity);

            else db.AnalysisDataSet.Add(this.Entity);

            db.SaveChanges();

            //Update the id of the current instance to keep the relation
            this.Id = Entity.Id;


            this.AnalysisInformation.Data = this;
            this.AnalysisInformation.Save();
        }
    }
}
