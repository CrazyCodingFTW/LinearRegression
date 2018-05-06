using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinearRegression.Database.ModelContracts
{
    public interface IAnalysisCalculations : IDBEntity
    {
        long AnalysisDataID { get; set; }

        string AdjustedY { get; set; }

        double B0 { get; set; }

        double B1 { get; set; }

        double ResidualDispersion { get; set; }

        double ExplainedDispersion { get; set; }

        double FEmpirical { get; set; }

        double FTheoretical { get; set; }

        double AverageErrorB0 { get; set; }

        double AverageErrorB1 { get; set; }

        double MaximalErrorB0 { get; set; }

        double MaximalErrorB1 { get; set; }

        IEnumerable<double> GetAdjustedYArray();

        void SetAdjustedYArray(IEnumerable<double> adjustedYs);
    }
}
