using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinearRegression.BusinessLogic
{
    public interface ILinearRegression
    {
        double Parameter0 { get; }
        double Parameter1 { get; }
        (double firstParameter, double secondParameter) GetRegressionEquation();
        List<double> GetAdjustedYsValues();
        bool CheckAdequacyOfModel();
        (double averageErrorOfFirstParameter, double averageErrorOfSecondParameter) GetAverageErrorOfParameters();
        (double maximumErrorOfFirstParameter, double maximumErrorOfSecondParameter) GetMaximumErrorOfParameters();
        (double lowerBoundOfFirstParameter, double upperBoundOfFirstParameter, double lowerBoundOfSecondParameter, double upperBoundOfSecondParameter) GetIntervalsOfRegressionParameters();
    }
}
