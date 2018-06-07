using LinearRegression.App.Contracts;
using LinearRegression.App.Contracts.Services;
using LinearRegression.App.Models;
using LinearRegression.Database;
using LinearRegression.Database.ModelAdapters;
using LinearRegression.Database.ModelContracts;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinearRegression.App.ServiceAdapters
{
    public class AnalysisLogicService : Service, IAnalysisLogicService
    {
        public AnalysisLogicService(IServiceProvider serviceProvider) : base(serviceProvider) { }

        private ModelController<LinearRegressionDbContext> controller = new ModelController<LinearRegressionDbContext>("LinearRegression.Database.Model");

        public IAnalysisData<IAdjustedDataRow> GetAdjustedData(IAnalysisData<IAnalysisDataRow> rawAnalysisModel)
        {
            //Returns X, Y and Y^ - adjusted Y.
   
            var fullAnalysis = (FullAnalysis<IAnalysisDataRow>)rawAnalysisModel;

            var analysisInfo = controller.GetEntityById<AnalysisInformation>(fullAnalysis.DatabaseId);
            var analysisData = analysisInfo.Data;

            var xdata = rawAnalysisModel.Data.Select(d => d.X).ToArray();
            var ydata = rawAnalysisModel.Data.Select(d => d.Y).ToArray();

            var businessLogicObject = new LinearRegression.BusinessLogic.Regression(xdata.ToList(), ydata.ToList());

            var adjustedYValues = new List<double>();

            //Checking if we already have this data in the database.
            if (controller.GetEntityById<AnalysisInformation>(fullAnalysis.DatabaseId) != null)
            {
                try
                {
                    //If we have this data, try to get the adjusted Y values from the analysis calculations.
                    adjustedYValues = analysisData.AnalysisCalculations.AdjustedY.ToList();
                }
                catch (Exception)
                {
                    //If we can't get the already calculated adjusted values, calculate them. 
                    adjustedYValues = businessLogicObject.GetAdjustedYsValues();
                }
            }
            else
            {
                //Else if we don't have the data calculate the adjusted Y values.
                adjustedYValues = businessLogicObject.GetAdjustedYsValues();
            }


            int iterator = 0;

            var adjustedDataRows = new ObservableCollection<IAdjustedDataRow>();
            foreach (var row in rawAnalysisModel.Data)
            {
                adjustedDataRows.Add(new AdjustedDataRow(row.Index, row.X, row.Y, adjustedYValues[iterator]));
                iterator++;
            }

            var adjustedAnalysis = new AdjustedAnalysis(rawAnalysisModel.XMeaning, rawAnalysisModel.YMeaning, adjustedDataRows);
            return adjustedAnalysis;
        }

        public IAnalysisCalculations GetAnalysisCalculations(IFullAnalysis<IAnalysisDataRow> analysis)
        {
           
            var xdata = analysis.Data.Select(d => d.X).ToArray();
            var ydata = analysis.Data.Select(d => d.Y).ToArray();

            var fullAnalysis = (FullAnalysis<IAnalysisDataRow>)analysis;

            var analysisInfo = controller.GetEntityById<AnalysisInformation>(fullAnalysis.DatabaseId);
            var analysisData = analysisInfo.Data;

            //Checking if we already have this data in the database.
            if (controller.GetEntityById<AnalysisInformation>(fullAnalysis.DatabaseId) != null)
            {
                //If we have this data, try to get the calculations from the analysis calculations.
                try
                {
                    var calculations = analysisData.AnalysisCalculations;
                    return calculations.Entity;
                }
                catch(Exception)
                {
                    //If we can't get the calculations, calculate them.
                    var businessLogicObject = new LinearRegression.BusinessLogic.Regression(xdata.ToList(), ydata.ToList());

                    var b0 = businessLogicObject.Parameter0;
                    var b1 = businessLogicObject.Parameter1;

                    var adjustedYs = businessLogicObject.GetAdjustedYsValues().ToArray();
                   
                    double residualDispersion = businessLogicObject.CheckAdequacyOfModel().residualDispersion;
                    double explainedDispersion = businessLogicObject.CheckAdequacyOfModel().explainedDispersion;
                    double fEmpirical = businessLogicObject.CheckAdequacyOfModel().fEmpirical;
                    double fTheoretical = businessLogicObject.CheckAdequacyOfModel().fTheoretical;

                    double averageErrorB0 = businessLogicObject.GetAverageErrorOfParameters().averageErrorOfFirstParameter;
                    double averageErrorB1 = businessLogicObject.GetAverageErrorOfParameters().averageErrorOfSecondParameter;

                    double maximalErrorB0 = businessLogicObject.GetMaximumErrorOfParameters().maximumErrorOfFirstParameter;
                    double maximalErrorB1 = businessLogicObject.GetMaximumErrorOfParameters().maximumErrorOfSecondParameter;

                    AnalysisCalculations c = new AnalysisCalculations(analysisData,adjustedYs,b0,b1,residualDispersion,explainedDispersion,
                        fEmpirical,fTheoretical,averageErrorB0,averageErrorB1,maximalErrorB0,maximalErrorB1,controller);

                    c.Save();
                    return c.Entity;
                }
            }
            else
            {
                //Else if we don't have the data, make the calculatons.

                var businessLogicObject = new LinearRegression.BusinessLogic.Regression(xdata.ToList(), ydata.ToList());

                var b0 = businessLogicObject.Parameter0;
                var b1 = businessLogicObject.Parameter1;

                var adjustedYs = businessLogicObject.GetAdjustedYsValues().ToArray();

                double residualDispersion = businessLogicObject.CheckAdequacyOfModel().residualDispersion;
                double explainedDispersion = businessLogicObject.CheckAdequacyOfModel().explainedDispersion;
                double fEmpirical = businessLogicObject.CheckAdequacyOfModel().fEmpirical;
                double fTheoretical = businessLogicObject.CheckAdequacyOfModel().fTheoretical;

                double averageErrorB0 = businessLogicObject.GetAverageErrorOfParameters().averageErrorOfFirstParameter;
                double averageErrorB1 = businessLogicObject.GetAverageErrorOfParameters().averageErrorOfSecondParameter;

                double maximalErrorB0 = businessLogicObject.GetMaximumErrorOfParameters().maximumErrorOfFirstParameter;
                double maximalErrorB1 = businessLogicObject.GetMaximumErrorOfParameters().maximumErrorOfSecondParameter;

                var calculation = new AnalysisCalculations(
                        analysisData, adjustedYs, b0, b1, residualDispersion, explainedDispersion,
                        fEmpirical, fTheoretical, averageErrorB0, averageErrorB1, maximalErrorB0, maximalErrorB1, controller
                    );

                calculation.Save();

                return calculation.Entity;


            }
            
        }

        public IFullAnalysis<IAdjustedDataRow> GetFullAnalysisAdjustedData(IFullAnalysis<IAnalysisDataRow> fullRawAnalysisModel)
        {
            //Returns all things from the regression analysis + adjusted Ys

            var adjustedData = GetAdjustedData(fullRawAnalysisModel);
            var fullAnalysisCast = (FullAnalysis<IAnalysisDataRow>)fullRawAnalysisModel;
            var fullAdjusted = new FullAnalysis<IAdjustedDataRow>(fullAnalysisCast.DatabaseId, fullRawAnalysisModel.Title, fullRawAnalysisModel.Description, fullRawAnalysisModel.XMeaning, fullRawAnalysisModel.YMeaning, adjustedData.Data);
            return fullAdjusted;
        }
    }
}
