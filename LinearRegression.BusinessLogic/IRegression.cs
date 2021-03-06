﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinearRegression.BusinessLogic
{
    public interface IRegression
    {
        double Parameter0 { get; }
        double Parameter1 { get; }
        (double firstParameter, double secondParameter) GetRegressionEquation();
        List<double> GetAdjustedYsValues();
        (double residualDispersion, double explainedDispersion, double fEmpirical, double fTheoretical, bool isModelAdecuate) CheckAdequacyOfModel();
        (double averageErrorOfFirstParameter, double averageErrorOfSecondParameter) GetAverageErrorOfParameters();
        (double maximumErrorOfFirstParameter, double maximumErrorOfSecondParameter) GetMaximumErrorOfParameters();
        (double lowerBoundOfFirstParameter, double upperBoundOfFirstParameter, double lowerBoundOfSecondParameter, double upperBoundOfSecondParameter) GetIntervalsOfRegressionParameters();
    }
}
