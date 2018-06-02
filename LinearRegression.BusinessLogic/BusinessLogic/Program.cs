using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MathNet.Numerics.Distributions;
using MathNet.Numerics.LinearRegression;
using System.Diagnostics;
using Accord.Statistics;

using System.Web;
using Accord.Statistics.Distributions.Univariate;
using Accord.Statistics.Testing;

namespace LinearRegression.BusinessLogic
{
    class Program
    {
        static void Main(string[] args)
        {
            //Console.WriteLine("Use the table of distributions for more info!!!");

            //var normalDistribution = new Normal();
            //var curvePercentage = normalDistribution.CumulativeDistribution(1.96);
            //Console.WriteLine($"z-score = {1.96}, % of curve = {curvePercentage} %");           

            //Console.WriteLine($"chi^2(alpha = {0.10}, df = {1}) = {ChiSquared.InvCDF(1,0.90)}");

            //FisherSnedecor fDistibution = new FisherSnedecor(1, 1);
            //Console.WriteLine($"F-Test (df1 = {1}, df2 = {8}, alpha = {0.05}) = {FisherSnedecor.InvCDF(1, 8, 0.95)}");




            //List<int> x2 = new List<int>() { 480, 360, 240, 150, 170 };
            //List<int> y2 = new List<int>() { 400, 300, 200, 100, 120 };

            //This example was taken from the excel file: РА в матрична форма.
            List<double> x2 = new List<double>() { 350, 1000, 1060, 2650, 210, 220, 1180, 470, 430, 400 };
            List<double> y2 = new List<double>() { 350, 620, 760, 1940, 210, 220, 610, 440, 430, 400 };

            ////Using SimpleRegression.Fit() to get the parameters of the regression equation.
            //var lr = SimpleRegression.Fit(x, y);
            //Console.WriteLine(lr.Item1);

            ////using my class LinearRgression to get the parameters
            LinearRegression lr2 = new LinearRegression(x2, y2);
            //Console.WriteLine(lr2.Parameter0);
            //Console.WriteLine(lr2.Parameter1);

            //Getting the theoretical values!!!
            //double[,] theoriticalValuesArray = lr2.GetAdjustedYsValues();

            //foreach (var item in theoriticalValuesArray)
            //{
            //    Console.WriteLine(item);
            //}

            //cheching if the model is adecuate!!!
            //Console.WriteLine(lr2.CheckAdequacyOfModel());

            ////Getting the average errors array
            //double[] test = lr2.GetAverageErrorOfParameters();

            //Console.WriteLine("average errors:");
            //foreach (var item in test)
            //{
            //    Console.WriteLine(item);
            //}

            ////Getting the maximum errors array
            //double[] test2 = lr2.GetMaximumErrorOfParameters();

            //Console.WriteLine("maximum errors:");
            //foreach (var item in test2)
            //{
            //    Console.WriteLine(item);
            //}

            //Getting the bounds of the first parameter
            var lowerBoundOfFirstParameter = lr2.GetIntervalsOfRegressionParameters().lowerBoundOfFirstParameter;
            var upperBoundOfFirstParameter = lr2.GetIntervalsOfRegressionParameters().upperBoundOfFirstParameter;

            //Getting the bounds of the second parameter
            var lowerBoundOfSecondParameter = lr2.GetIntervalsOfRegressionParameters().lowerBoundOfSecondParameter;
            var upperBoundOfSecondParameter = lr2.GetIntervalsOfRegressionParameters().upperBoundOfSecondParameter;

            Console.WriteLine(lowerBoundOfFirstParameter);
            Console.WriteLine(upperBoundOfFirstParameter);
            Console.WriteLine(lowerBoundOfSecondParameter);
            Console.WriteLine(upperBoundOfSecondParameter);

            //var tDistribution = new TDistribution(1);
            //var tTest = new TTest(1,2);

            //Console.WriteLine(tTest.CriticalValue);
        }
    }
}
