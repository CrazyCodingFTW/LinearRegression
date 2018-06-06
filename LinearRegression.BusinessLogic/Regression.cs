using MathNet.Numerics.Distributions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinearRegression.BusinessLogic
{
    public class Regression : IRegression
    {
        private List<double> XValues;
        private List<double> YValues;

        //This variable is used to check if the sum of Y^ is equal to Y.
        //Because this is how we know that the regression equation is correct.
        private bool areAdjustedYValuesAlright = false;

        public Regression()
        {

        }

        public Regression(List<double> XValues, List<double> YValues)
        {
            this.XValues = XValues;
            this.YValues = YValues;
        }

        private int Count
        {
            get
            {
                return this.XValues.Count;
            }
        }

        public double Parameter0
        {
            get
            {
                return this.GetRegressionEquation().firstParameter;
            }
        }


        public double Parameter1
        {
            get
            {
                return this.GetRegressionEquation().secondParameter;
            }
        }

        public (double firstParameter, double secondParameter) GetRegressionEquation()
        {

            //Formula is: b1 = ((n * Sum(Xi*yi)) - (Sum(Xi) * Sum(Yi))) / ((n * Sum(xi^2)) - sum(Xi)^2 )
            double b1 = ((this.Count * GetSumOfXiYi()) - (GetSumOfXi() * GetSumOfYi())) / ((this.Count * GetSumOfXiSquared()) - Math.Pow(GetSumOfXi(), 2));
            double b0 = (GetSumOfYi() / this.Count) - (b1 * (GetSumOfXi() / this.Count));

            if (Double.IsNaN(b0) || Double.IsInfinity(b0))
            {
                b0 = 0;
            }

            if (Double.IsNaN(b1) || Double.IsInfinity(b1))
            {
                b1 = 0;
            }

            return (b0, b1);
        }

        private double GetSumOfXiSquared()
        {
            double sumOfXiSquared = 0.0D;

            for (int i = 0; i < XValues.Count; i++)
            {
                sumOfXiSquared += Math.Pow(this.XValues[i], 2);
            }

            return sumOfXiSquared;
        }

        private double GetSumOfYi()
        {
            double sumOfYi = 0.0D;

            for (int i = 0; i < YValues.Count; i++)
            {
                sumOfYi += this.YValues[i];
            }

            return sumOfYi;
        }

        private double GetSumOfXi()
        {
            double sumOfXi = 0.0D;

            for (int i = 0; i < XValues.Count; i++)
            {
                sumOfXi += this.XValues[i];
            }

            return sumOfXi;
        }

        private double GetSumOfXiYi()
        {
            double sumOfXiYi = 0.0D;

            for (int i = 0; i < XValues.Count; i++)
            {
                sumOfXiYi += (this.XValues[i] * this.YValues[i]);
            }

            return sumOfXiYi;
        }

        public List<double> GetAdjustedYsValues()
        {
            List<double> adjustedValues = new List<double>();

            for (int i = 0; i < this.XValues.Count; i++)
            {
                adjustedValues.Add(this.Parameter0 + (this.Parameter1 * this.XValues[i]));
            }

            if (Math.Round(adjustedValues.Sum(), 0) == Math.Round(this.GetSumOfYi(), 0))
            {
                areAdjustedYValuesAlright = true;
                return adjustedValues;
            }
            else
            {
                areAdjustedYValuesAlright = false;
                return new List<double>() { 0 };
            }
        }

        public (double residualDispersion, double explainedDispersion, double fEmpirical, double fTheoretical, bool isModelAdecuate) CheckAdequacyOfModel()
        {
            //If the adjustedValues are correct continue with the analysis
            if (areAdjustedYValuesAlright == true)
            {
                double alpha = 0.05D;
                double numberOfEquationParameters = 2.0D;

                double explainedDeviation = this.GetExplainedDeviation();

                double residualDeviation = this.GetResidualDeviation();

                double explainedDisperssion = explainedDeviation / (numberOfEquationParameters - 1.0D);

                if (Double.IsNaN(explainedDisperssion) || Double.IsInfinity(explainedDisperssion))
                {
                    explainedDisperssion = 1;
                }

                double residualDisperssion = residualDeviation / (this.YValues.Count - numberOfEquationParameters);

                if (Double.IsNaN(residualDisperssion) || Double.IsInfinity(residualDisperssion))
                {
                    residualDisperssion = 1;
                }

                double FEmpirical = (explainedDisperssion >= residualDisperssion) ? (explainedDisperssion / residualDisperssion) : (residualDisperssion / explainedDisperssion);

                if (Double.IsNaN(FEmpirical) || Double.IsInfinity(FEmpirical))
                {
                    FEmpirical = 1;
                }

                double firstDegreeOfFreedom = 0.0;
                double secondDegreeOfFreedom = 0.0;

                if (explainedDisperssion > residualDisperssion)
                {
                    firstDegreeOfFreedom = (numberOfEquationParameters - 1);
                    secondDegreeOfFreedom = (this.Count - numberOfEquationParameters);
                }
                else if (residualDisperssion > explainedDisperssion)
                {
                    firstDegreeOfFreedom = (this.Count - numberOfEquationParameters);
                    secondDegreeOfFreedom = (numberOfEquationParameters - 1);
                }
                else
                {
                    firstDegreeOfFreedom = (numberOfEquationParameters - 1);
                    secondDegreeOfFreedom = (this.Count - numberOfEquationParameters);
                }

                if (firstDegreeOfFreedom <= 0)
                {
                    firstDegreeOfFreedom = 1;
                }

                if (secondDegreeOfFreedom <= 0)
                {
                    secondDegreeOfFreedom = 1;
                }


                FisherSnedecor fDistibution = new FisherSnedecor(1, 1);
                double FTheoretical = FisherSnedecor.InvCDF(firstDegreeOfFreedom, secondDegreeOfFreedom, (1.00 - alpha));

                bool isModelAdequate = false;

                if (FEmpirical <= FTheoretical)
                {
                    isModelAdequate = false;
                }
                else if (FEmpirical > FTheoretical)
                {
                    isModelAdequate = true;
                }

                return (residualDisperssion, explainedDisperssion, FEmpirical, FTheoretical, isModelAdequate);
            }
            else
            {
                //Else return default values.
                return (0.0, 0.0, 0.0, 0.0, false);
            }



        }

        private double GetResidualDeviation()
        {
            var adjustedYValues = this.GetAdjustedYsValues();

            double residualDeviation = 0.0D;

            for (int i = 0; i < this.YValues.Count; i++)
            {
                residualDeviation += Math.Pow((this.YValues[i] - adjustedYValues[i]), 2);
            }

            return residualDeviation;
        }


        private double GetExplainedDeviation()
        {
            double averageY = this.GetAverageY();

            var adjustedYValues = this.GetAdjustedYsValues();

            double explainedDeviation = 0.0;

            for (int i = 0; i < adjustedYValues.Count; i++)
            {
                explainedDeviation += Math.Pow((adjustedYValues[i] - averageY), 2);
            }

            return explainedDeviation;

        }

        private double GetAverageY()
        {
            double sum = 0.0D;

            for (int i = 0; i < this.YValues.Count; i++)
            {
                sum += this.YValues[i];
            }

            return (sum / this.XValues.Count);
        }

        public (double averageErrorOfFirstParameter, double averageErrorOfSecondParameter) GetAverageErrorOfParameters()
        {
            //If regression model is adequate
            if (this.CheckAdequacyOfModel().isModelAdecuate == true)
            {
                //Random disperssion = residual disperssion if model is adecuate!!!
                var randomDisperssion = this.CheckAdequacyOfModel().residualDispersion;

                double averageErrorOfB0 = Math.Sqrt(randomDisperssion * (this.GetSumOfXiSquared() / (this.Count * this.GetSumOfDifferenceOfXiAndXAverageSquared())));

                double averageErrorOfB1 = Math.Sqrt(randomDisperssion / this.GetSumOfDifferenceOfXiAndXAverageSquared());

                return (averageErrorOfB0, averageErrorOfB1);

            }
            else
            {
                //If regression model is not adecuate return 0 as average error of both B0 and B1.
                return (0.0D, 0.0D);
            }

        }

        private double GetSumOfDifferenceOfXiAndXAverageSquared()
        {
            double sum = 0.0D;

            for (int i = 0; i < this.Count; i++)
            {
                sum += Math.Pow((this.XValues[i] - this.GetAverageX()), 2);
            }

            return sum;
        }

        private double GetAverageX()
        {
            double sum = 0.0;

            for (int i = 0; i < this.XValues.Count; i++)
            {
                sum += this.XValues[i];
            }

            return (sum / this.XValues.Count);
        }

        public (double maximumErrorOfFirstParameter, double maximumErrorOfSecondParameter) GetMaximumErrorOfParameters()
        {
            //If regression model is adequate
            if (this.CheckAdequacyOfModel().isModelAdecuate == true)
            {
                //Alpha is 0.05 => P(z) = 1 - alpha = 0.95. When P(z) is 0.95 the z-score is 1.96 form the z distribution table.
                double zScore = 1.96D;

                double maximalErrorOfB0 = zScore * this.GetAverageErrorOfParameters().averageErrorOfFirstParameter;
                double maximalErrorOfB1 = zScore * this.GetAverageErrorOfParameters().averageErrorOfSecondParameter;

                return (maximalErrorOfB0, maximalErrorOfB1);
            }
            else
            {
                //If regression model is not adecuate return 0 as average error of both B0 and B1.
                return (0.0D, 0.0D);
            }
        }

        public (double lowerBoundOfFirstParameter, double upperBoundOfFirstParameter, double lowerBoundOfSecondParameter, double upperBoundOfSecondParameter) GetIntervalsOfRegressionParameters()
        {
            //If regression model is adequate
            if (this.CheckAdequacyOfModel().isModelAdecuate == true)
            {
                var lowerBoundOfB0 = this.Parameter0 - this.GetMaximumErrorOfParameters().maximumErrorOfFirstParameter;
                var upperBoundOfB0 = this.Parameter0 + this.GetMaximumErrorOfParameters().maximumErrorOfFirstParameter;

                var lowerBoundOfB1 = this.Parameter1 - this.GetMaximumErrorOfParameters().maximumErrorOfSecondParameter;
                var upperBoundOfB1 = this.Parameter1 + this.GetMaximumErrorOfParameters().maximumErrorOfSecondParameter;

                return (lowerBoundOfB0, upperBoundOfB0, lowerBoundOfB1, upperBoundOfB1);
            }
            else
            {
                //If regression model is not adecuate return 0 as average error of both B0 and B1.
                return (0.0D, 0.0D, 0.0D, 0.0D);
            }
        }
    }
}
