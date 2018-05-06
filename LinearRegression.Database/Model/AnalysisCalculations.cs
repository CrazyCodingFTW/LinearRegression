using LinearRegression.Database.ModelContracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinearRegression.Database.Model
{
    public class AnalysisCalculations : IAnalysisCalculations
    {
        public AnalysisCalculations() { }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="analysisDataId"></param>
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
        public AnalysisCalculations
            (long analysisDataId, IEnumerable<double> adjustedYs, double b0, double b1, double residualDispersion, double explainedDispersion, double fEmpirical, double fTheoretical, double avgErrorB0, double avgErrorB1, double maxErrorB0, double maxErrorB1)
        {
            this.AnalysisDataID = analysisDataId;

            this.B0 = b0;
            this.B1 = b1;

            this.ResidualDispersion = residualDispersion;

            this.ExplainedDispersion = explainedDispersion;

            this.FEmpirical = fEmpirical;
            this.FTheoretical = FTheoretical;

            this.AverageErrorB0 = avgErrorB0;
            this.AverageErrorB1 = avgErrorB1;

            this.MaximalErrorB0 = maxErrorB0;
            this.MaximalErrorB1 = maxErrorB1;

            SetAdjustedYArray(adjustedYs);
        }

        public long Id { get; set; }
        public long AnalysisDataID { get; set; }
        public string AdjustedY { get; set; }
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

        public double MaximalErrorB1 { get; set ; }

        public IEnumerable<double> GetAdjustedYArray()
        {
            var array = this.AdjustedY.Split(" ".ToCharArray(), StringSplitOptions.RemoveEmptyEntries).Select(double.Parse).ToArray();

            return array;
        }

        public void SetAdjustedYArray(IEnumerable<double> array)
        {
            var result = string.Join(" ", array);
            this.AdjustedY = result;
        }
    }
}
