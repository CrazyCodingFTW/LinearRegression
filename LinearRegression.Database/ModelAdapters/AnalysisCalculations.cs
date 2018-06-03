using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LinearRegression.Database.Model;
using LinearRegression.Database.ModelContracts;

namespace LinearRegression.Database.ModelAdapters
{
    public class AnalysisCalculations : ModelAdapter<Model.AnalysisCalculations>
    {
        private AnalysisData analysisData;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="analysisData"></param>
        /// <param name="adjustedYs"></param>
        /// <param name="b0"></param>
        /// <param name="b1"></param>
        /// <param name="residualDispersion">The lesser dispersion</param>
        /// <param name="explainedDispersion">The greater dispersion</param>
        /// <param name="fEmpirical"></param>
        /// <param name="fTheoretical"></param>
        /// <param name="avgErrorB0"></param>
        /// <param name="avgErrorB1"></param>
        /// <param name="maxErrorB0"></param>
        /// <param name="maxErrorB1"></param>
        /// <param name="controller"></param>
        public AnalysisCalculations
            (AnalysisData analysisData, IEnumerable<double> adjustedYs, double b0, double b1, double residualDispersion, double explainedDispersion, double fEmpirical, double fTheoretical, double avgErrorB0, double avgErrorB1, double maxErrorB0, double maxErrorB1, IModelController<LinearRegressionDbContext> controller) : base(controller)
        {
            this.AnalysisData = analysisData;

            this.B0 = b0;
            this.B1 = b1;

            this.AdjustedY = adjustedYs;

            this.ResidualDispersion = residualDispersion;

            this.ExplainedDispersion = explainedDispersion;

            this.FEmpirical = fEmpirical;
            this.FTheoretical = fTheoretical;

            this.AverageErrorB0 = avgErrorB0;
            this.AverageErrorB1 = avgErrorB1;

            this.MaximalErrorB0 = maxErrorB0;
            this.MaximalErrorB1 = maxErrorB1;
        }

        public AnalysisCalculations(Model.AnalysisCalculations entity, IModelController<LinearRegressionDbContext> controller) : base(entity, controller)
        {
            this.AnalysisData = controller.GetEntityById<AnalysisData>(entity.AnalysisDataID);

            this.B0 = entity.B0;
            this.B1 = entity.B1;

            this.AdjustedY = entity.GetAdjustedYArray();

            this.ResidualDispersion = entity.ResidualDispersion;

            this.ExplainedDispersion = entity.ExplainedDispersion;

            this.FEmpirical = entity.FEmpirical;
            this.FTheoretical = entity.FTheoretical;

            this.AverageErrorB0 = entity.AverageErrorB0;
            this.AverageErrorB1 = entity.AverageErrorB1;

            this.MaximalErrorB0 = entity.MaximalErrorB0;
            this.MaximalErrorB1 = entity.MaximalErrorB1;
        }

        public AnalysisData AnalysisData
        {
            get
            {
                if (this.analysisData is null)
                    this.analysisData = this.Controller.GetEntityById<AnalysisData>(this.Entity.AnalysisDataID);

                return this.analysisData;
            }

            private set => this.analysisData = value;
        }

        public IEnumerable<double> AdjustedY { get; set; }
        public double B0 { get; set; }
        public double B1 { get; set; }

        /// <summary>
        /// The lesser dispersion
        /// </summary>
        public double ResidualDispersion { get; set; }

        /// <summary>
        /// The greater dispersion
        /// </summary>
        public double ExplainedDispersion { get; set; }

        public double FEmpirical { get; set; }

        public double FTheoretical { get; set; }

        public double AverageErrorB0 { get; set; }

        public double AverageErrorB1 { get; set; }

        public double MaximalErrorB0 { get; set; }

        public double MaximalErrorB1 { get; set; }

        public override void Delete()
        {
            throw new InvalidOperationException("To delete analysis calculations, you must delete all the data for the analysis!");
        }

        public override void Delete(LinearRegressionDbContext db)
        {
            this.Delete();
        }

        public override void Save(LinearRegressionDbContext db)
        {
            if (this.analysisData is null || this.analysisData.Id == 0)
                throw new InvalidOperationException("Cannot set the calculations to an unsaved analysis data entity");

            if (this.Entity is null)
                this.Entity = new Model.AnalysisCalculations(this.analysisData.Id, this.AdjustedY, this.B0, this.B1, this.ResidualDispersion, this.ExplainedDispersion, this.FEmpirical, this.FTheoretical, this.AverageErrorB0, this.AverageErrorB1, this.MaximalErrorB0, this.MaximalErrorB1);

            else
            {

                this.Entity.B0 = this.B0;
                this.Entity.B1 = this.B0;

                this.Entity.ResidualDispersion = this.ResidualDispersion;

                this.Entity.ExplainedDispersion = this.ExplainedDispersion;

                this.Entity.FEmpirical = this.FEmpirical;
                this.Entity.FTheoretical = this.FTheoretical;

                this.Entity.AverageErrorB0 = this.AverageErrorB0;
                this.Entity.AverageErrorB1 = this.AverageErrorB1;

                this.Entity.MaximalErrorB0 = MaximalErrorB0;
                this.Entity.MaximalErrorB1 = MaximalErrorB1;

                this.Entity.SetAdjustedYArray(this.AdjustedY);
            }

            if (db.AnalysisCalculationsSet.Any(ac => ac == this.Entity))
                db.Update(this.Entity);

            else db.Add(this.Entity);

            db.SaveChanges();

            this.Id = this.Entity.Id;

            this.AnalysisData.Entity.AnalysisCalculationsId = this.Id;

            db.Update(this.AnalysisData.Entity);
            db.SaveChanges();
        }
    }
}
