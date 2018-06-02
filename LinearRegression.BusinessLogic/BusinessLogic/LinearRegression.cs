using MathNet.Numerics.Distributions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinearRegression.BusinessLogic
{
   public class LinearRegression : ILinearRegression
    {
        private List<double> XValues;
        private List<double> YValues;

        private double[,] XArray;
        private double[,] YArray;

        private Matrix XMatrix;
        private Matrix YMatrix;

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

        public LinearRegression()
        {

        }

        public LinearRegression(List<double> XValues, List<double> YValues)
        {
            this.XValues = XValues;
            this.YValues = YValues;
        }

        private void ConvertXValuesToCorrectMatrix()
        {
            this.XArray = new double[this.XValues.Count, 2];

            for (int i = 0; i < this.XValues.Count; i++)
            {
                for (int j = 0; j < 2; j++)
                {
                    if (j == 0)
                    {
                        this.XArray[i, j] = 1;
                    }
                    else if (j == 1)
                    {
                        this.XArray[i, j] = this.XValues[i];
                    }
                }
            }
        }

        private double[,] GetXArray()
        {
            this.ConvertXValuesToCorrectMatrix();
            return this.XArray;
        }

        private void ConvertYValuesToCorrectMatrix()
        {
            this.YArray = new double[this.YValues.Count, 1];

            for (int i = 0; i < this.YValues.Count; i++)
            {
                this.YArray[i, 0] = this.YValues[i];
            }
        }

        private double[,] GetYArray()
        {
            this.ConvertYValuesToCorrectMatrix();
            return this.YArray;
        }

        public (double firstParameter, double secondParameter) GetRegressionEquation()
        {
            //Formula for the two parameters of the equation is: B = (X'X)^-1 * (X'Y)

            //*** Initizlize the XArray and YArray using GetX/YArray()
            //so that we can use it for X/YMatrix!!! ***
            this.XArray = this.GetXArray();
            this.YArray = this.GetYArray();

            //Initialize the X and Y matricies
            //X's size is always nx2. Y's size is always nx1
            this.XMatrix = Matrix.ConvertArrayToMatrix(this.XArray, this.XValues.Count, 2);
            this.YMatrix = Matrix.ConvertArrayToMatrix(this.YArray, this.XValues.Count, 1);

            //1. Solve the first part of the equation: (X'X)^-1

            //Transponse the XMatrix and convert it to Matrix type because 
            //TransponceMatrix() returns double [,]
            //but we need Matrix type in order to make the multiplication!!!
            Matrix XTransponedMatrix = Matrix.ConvertArrayToMatrix(
                    Matrix.TransponceMatrix(this.XMatrix), 2, this.XValues.Count
                );

            //Multiplication of X'X . It's size is always 2x2.
            double[,] multiplicationOfXPrimeX = Matrix.MultiplyMatrices(XTransponedMatrix, XMatrix);

            //Getting the inverse of X'X. It's size is always 2x2!!!
            double[,] invertedXPrimeX = Matrix.Inverse2By2Matrix(
                Matrix.ConvertArrayToMatrix(multiplicationOfXPrimeX, 2, 2)
                );

            //2. Solving the second part of the equation: X'Y

            //Multiplication of X'Y. It's size is always 2x1!!!
            double[,] multiplicationOfXPrimeY = Matrix.MultiplyMatrices(XTransponedMatrix, this.YMatrix);

            //3. Finally multiplication of (X'X)^-1 and (X'Y)
            //It is saved in parametersOfEquation array. It's size is always 2x1.

            double[,] parametersOfEquation = Matrix.MultiplyMatrices(
                Matrix.ConvertArrayToMatrix(invertedXPrimeX, 2, 2),
                Matrix.ConvertArrayToMatrix(multiplicationOfXPrimeY, 2, 1)
                );

            var B0 = parametersOfEquation[0, 0];
            var B1 = parametersOfEquation[1, 0];

            return (B0,B1);

        }

        //We need this method in order to use the parameters for the Matrix class methods.
        private double [,] GetRegressionEquationParametersAsArray()
        {
            var firstParameterOfEquation = this.GetRegressionEquation().firstParameter;
            var secondParameterOfEquation = this.GetRegressionEquation().secondParameter;

            return new double[,]
            {
                {firstParameterOfEquation},
                {secondParameterOfEquation }
            };

        }

        public List<double> GetAdjustedYsValues()
        {
            var parametersOfEquation = this.GetRegressionEquation();

            var parametersArray = new double[,] {
                { parametersOfEquation.firstParameter },
                {parametersOfEquation.secondParameter }
            };

            double[,] theoriticalValuesMatrix = Matrix.MultiplyMatrices(
                this.XMatrix,
                Matrix.ConvertArrayToMatrix(parametersArray, 2, 1)
                );

            List<double> theoriticalValues = new List<double>();

            foreach (var item in theoriticalValuesMatrix)
            {
                theoriticalValues.Add(item);
            }

            return theoriticalValues;
        }

        public bool CheckAdequacyOfModel()
        {
            double alpha = 0.05D;

            double unexplainedDeviation = this.GetUnexplainedDeviation();
            double explainedDeviation = this.GetExplainedDeviation();

            //Formula is S2 / (n - p); p -> the number of parameters of the regression equation. Always 2!!!
            double unexplainedDisperssion = unexplainedDeviation / (this.XValues.Count - 2);

            //Formula is S1 / (p-1); p -> the number of parameters of the regression equation. Always 2!!!
            double explainedDisperssion = explainedDeviation / (2 - 1);

            //Fem
            double FEmpiricaly = (explainedDisperssion > unexplainedDisperssion) ? (explainedDisperssion / unexplainedDisperssion) : (unexplainedDisperssion / explainedDisperssion);

            //Calculating the degrees of freedom for the F-distribution theoritical value.
            double firstDegreeOfFreedom = (explainedDisperssion > unexplainedDisperssion) ? 1 : (this.XValues.Count - 2);
            double secondDegreeOfFreedom = (explainedDisperssion > unexplainedDisperssion) ? (this.XValues.Count - 2) : 1;

            //Getting the F-Distiribution critical value.
            FisherSnedecor fDistibution = new FisherSnedecor(1, 1);
            double FTheoretically = FisherSnedecor.InvCDF(firstDegreeOfFreedom, secondDegreeOfFreedom, (1.00 - alpha));
            //Console.WriteLine($"F-Test (df1 = {1}, df2 = {8}, alpha = {0.05}) = {FisherSnedecor.InvCDF(firstDegreeOfFreedom, secondDegreeOfFreedom, 1 - alpha)}");

            //Checking to see which hypothesis is correct
            if (FEmpiricaly > FTheoretically)
            {
                return true;//H1: model is adecuate.
            }
            else
            {
                return false;//H0: model IS NOT adecuate.
            }

        }

        private double GetUnexplainedDeviation()
        {
            //The formula is this: Y'Y - B'X'Y

            //The first part of the equation Y'Y:

            //Getting the Y'Y. It's size is always 1X1
            double[,] YTransponsed = Matrix.TransponceMatrix(this.YMatrix);

            //It's size is always 1x1!! Y'Y
            double[,] firstPartOfTheEquation = Matrix.MultiplyMatrices(
                    Matrix.ConvertArrayToMatrix(YTransponsed, 1, this.YValues.Count),
                    this.YMatrix
                );

            //Second part of the equation B'X'Y:

            //Getting B matrix
            //It was this.GetRegressionEquation() but it no longer returns double [,], so we need the method below;
            double[,] parametersOfEquation = this.GetRegressionEquationParametersAsArray();

            //Getting B' matrix 1x2 always
            double[,] transponcedEquationParameters = Matrix.TransponceMatrix(
                Matrix.ConvertArrayToMatrix(parametersOfEquation, 2, 1)
            );

            //Getting X' matrix

            double[,] transponcedXArray = Matrix.TransponceMatrix(this.XMatrix);

            //Although it is an 2-dimmentional array it consists of only 1 element at index [0,0]
            //Second part of the equation B'X'Y
            double[,] secondPartOfEquation =
                Matrix.MultiplyMatrices(
                //B' Matrix
                (Matrix.ConvertArrayToMatrix(transponcedEquationParameters, 1, 2)),
                 //X'Y multiplication
                 Matrix.ConvertArrayToMatrix(
                    // the size is always 2x1!!!
                    Matrix.MultiplyMatrices(
                        Matrix.ConvertArrayToMatrix(transponcedXArray, 2, this.XValues.Count),
                        Matrix.ConvertArrayToMatrix(this.YArray, this.YValues.Count, 1)
                        ), 2, 1
                    )
                );

            //Finally the result
            double expectedDeviation = firstPartOfTheEquation[0, 0] - secondPartOfEquation[0, 0];
            return expectedDeviation;
        }

        private double GetExplainedDeviation()
        {
            //The formula is this: B'X'Y - (((SUM(Yi))^2)/n)
            //The first part of the equation B'X'Y:

            //Getting B matrix
            //It was this.GetRegressionEquation() but it no longer returns double [,], so we need the method below;
            double[,] parametersOfEquation = this.GetRegressionEquationParametersAsArray();

            //Getting B' matrix 1x2 always
            double[,] transponcedEquationParameters = Matrix.TransponceMatrix(
                Matrix.ConvertArrayToMatrix(parametersOfEquation, 2, 1)
            );

            //Getting X' matrix

            double[,] transponcedXArray = Matrix.TransponceMatrix(this.XMatrix);

            //Although it is an 2-dimmentional array it consists of only 1 element at index [0,0]
            //Second part of the equation B'X'Y
            double[,] firstPartOfEquation =
                Matrix.MultiplyMatrices(
                //B' Matrix
                (Matrix.ConvertArrayToMatrix(transponcedEquationParameters, 1, 2)),
                 //X'Y multiplication
                 Matrix.ConvertArrayToMatrix(
                    // the size is always 2x1!!!
                    Matrix.MultiplyMatrices(
                        Matrix.ConvertArrayToMatrix(transponcedXArray, 2, this.XValues.Count),
                        Matrix.ConvertArrayToMatrix(this.YArray, this.YValues.Count, 1)
                        ), 2, 1
                    )
                );

            //Second part of the equation ((SUM(Yi)^2)/n):
            double secondPartOfEquation = Math.Pow(
                Matrix.SumOfElementsOf1ColumnedMatrices(this.YValues.ToArray())
                , 2
                ) / this.YValues.Count;

            //Finally the result
            double expectedDisperssion = firstPartOfEquation[0, 0] - secondPartOfEquation;

            return expectedDisperssion;

        }

        public (double averageErrorOfFirstParameter, double averageErrorOfSecondParameter) GetAverageErrorOfParameters()
        {
            //getting the X' matrix

            double[,] XTransponced = Matrix.TransponceMatrix(this.XMatrix);

            //Getting the X'X matrix. Size: 2x2 always!!!
            double[,] XTransponcedX = Matrix.MultiplyMatrices(
                Matrix.ConvertArrayToMatrix(XTransponced, 2, this.XValues.Count),
                this.XMatrix
                );

            //Inverting the X'X Matrix. Size is always 2x2!!!
            double[,] invertedXTrX = Matrix.Inverse2By2Matrix(
                Matrix.ConvertArrayToMatrix(XTransponcedX, 2, 2)
                );

            //If model is not adecuate 
            if (!this.CheckAdequacyOfModel())
            {
                throw new InvalidOperationException();
            }

            //Getting the unexplained deviation in order to get the unexpected disperssion.
            double unexplainedDeviation = this.GetUnexplainedDeviation();

            // Formula is S2 / n - p->the number of parameters of the regression equation.
            double unexplainedDisperssion = unexplainedDeviation / (this.XValues.Count - 2);

            //Calculating the averege error of first parameter: sqrt(σ2^2 * C00)
            double averageErrorOfFirstParameter = Math.Sqrt(unexplainedDisperssion * invertedXTrX[0, 0]);

            //Calculating the averege error of second parameter: sqrt(σ2^2 * C11)
            double averageErrorOfSecondParameter = Math.Sqrt(unexplainedDisperssion * invertedXTrX[1, 1]);

            //Returning the two average errors
            return ( averageErrorOfFirstParameter, averageErrorOfSecondParameter );
        }

        public (double maximumErrorOfFirstParameter, double maximumErrorOfSecondParameter) GetMaximumErrorOfParameters()
        {
            //Because Alpha is 5% => P(z) = 95% => z = 1.96
            double zScore = 1.96;

            var averageErrorOfParameters = this.GetAverageErrorOfParameters();

            ////The formula is average error of b0 *z; average error of b1 * z
            var maximumErrorOfFirstParam = averageErrorOfParameters.averageErrorOfFirstParameter * zScore;
            var maximumErrorOfSecondParam = averageErrorOfParameters.averageErrorOfSecondParameter * zScore;

            
            return (maximumErrorOfFirstParam, maximumErrorOfSecondParam);
        }

        public (double lowerBoundOfFirstParameter, double upperBoundOfFirstParameter, double lowerBoundOfSecondParameter, double upperBoundOfSecondParameter) GetIntervalsOfRegressionParameters()
        {
            var regressionEquationParameters = this.GetRegressionEquation();

            //double[] maximumErrorsOfParameters = this.GetMaximumErrorOfParameters();

            //Getting the maximumErrors of the parameters.
            double maximumErrorOfFirstParameter = this.GetMaximumErrorOfParameters().maximumErrorOfFirstParameter;
            double maximumErrorOfSecondParameter = this.GetMaximumErrorOfParameters().maximumErrorOfSecondParameter;

            //Getting the lower bound of b0 = b0 - maxError(b0)
            var lowerBoundOfFirstParam = this.GetRegressionEquation().firstParameter - maximumErrorOfFirstParameter;

            //Getting the upper bound of b0 = b0 + maxError(b0)
            var upperBoundOfFirstParam = this.GetRegressionEquation().firstParameter + maximumErrorOfFirstParameter;


            //Getting the lower bound of b1 = b1 - maxError(b1)
            var lowerBoundOfSecondParam = this.GetRegressionEquation().secondParameter - maximumErrorOfSecondParameter;

            //Getting the lower bound of b1 = b1 + maxError(b1)
            var upperBoundOfSecondParam = this.GetRegressionEquation().secondParameter + maximumErrorOfSecondParameter;

            return (lowerBoundOfFirstParam, upperBoundOfFirstParam, lowerBoundOfSecondParam, upperBoundOfSecondParam);
        }
    }
}
