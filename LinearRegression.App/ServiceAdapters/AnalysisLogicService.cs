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

        private List<double> adjustedYsList = new List<double>();

        private bool areAdjustedYsCalculated = false;

        public IAnalysisData<IAdjustedDataRow> GetAdjustedData(IAnalysisData<IAnalysisDataRow> rawAnalysisModel)
        {
            //Return X,Y and Y^ - adjusted Y.

            //TODO: First check if the database contains the analysis
            //var xdata = rawAnalysisModel.Data.Select(d => d.X).ToArray();
            //var ydata = rawAnalysisModel.Data.Select(d => d.Y).ToArray();

            //List<double> ys = NikiService.GetAdjustedY(xdata, ydata);

            //Creating business logic object to use the business logic methods!!!
            //var businessLogicObject = new LinearRegression.BusinessLogic.Regression(xdata.ToList(),ydata.ToList());

            //Getting the adjusted Y values.
            // var adjustedYValues = businessLogicObject.GetAdjustedYsValues();

            //Creating observable collection with Xs, Ys and adjusted Ys.
            //var collection = new ObservableCollection<IAdjustedDataRow>();

            //for (int i = 0; i < adjustedYValues.Count; i++)
            //    collection.Add(new AdjustedDataRow(i + 1, xdata[i], ydata[i], adjustedYValues[i]));

            //return new FullAnalysis<IAdjustedDataRow>(12, null, null, null, null, collection);

            //TODO: use calculations to get the adjusted data table or check if it already exists in the database
            //throw new NotImplementedException();

            //TODO: Find adjusted values

            var fullAnalysis = (FullAnalysis<IAnalysisDataRow>)rawAnalysisModel;

            var analysisInfo = controller.GetEntityById<AnalysisInformation>(fullAnalysis.DatabaseId);
            var analysisData = analysisInfo.Data;

            if (controller.GetEntityById<AnalysisCalculations>(fullAnalysis.DatabaseId) != null)
            {

            }
            else
            {

            }

            var adjustedDataRows = new ObservableCollection<IAdjustedDataRow>();
            foreach (var row in rawAnalysisModel.Data)
                adjustedDataRows.Add(new AdjustedDataRow(row.Index, row.X, row.Y, row.Y));

            var adjustedAnalysis = new AdjustedAnalysis(rawAnalysisModel.XMeaning, rawAnalysisModel.YMeaning, adjustedDataRows);
            return adjustedAnalysis;
        }

        public IAnalysisCalculations GetAnalysisCalculations(IFullAnalysis<IAnalysisDataRow> analysis)
        {
            //TODO: Check if the data had already been calculated or generate new calculations.
            var xdata = analysis.Data.Select(d => d.X).ToArray();
            var ydata = analysis.Data.Select(d => d.Y).ToArray();

            // var databaseId = analysis.DatabaseId;

            controller = new ModelController<LinearRegressionDbContext>("LinearRegression.Database.Model");

            //var analysisInfo = new AnalysisInformation(analysis.CreationDate,analysis.Title,analysis.Description,controller);

            //var analysisData = new AnalysisData(analysis.XMeaning, xdata, analysis.YMeaning, ydata, analysisInfo, controller);

            //analysisInfo.Data = analysisData;

            var fullAnalysis = (FullAnalysis<IAnalysisDataRow>)analysis;

            var analysisInfo = controller.GetEntityById<AnalysisInformation>(fullAnalysis.DatabaseId);
            var analysisData = analysisInfo.Data;

            //TODO: Check if the calculations has already been calculated and is in the database.
            if (controller.GetEntityById<AnalysisInformation>(fullAnalysis.DatabaseId) != null)
            {
                //var analysisCalculations = controller.GetEntityById<AnalysisCalculations>(fullAnalysis.DatabaseId);
               // return analysisCalculations.Entity;

                try
                {
                    var calculations = analysisData.AnalysisCalculations;
                    return calculations.Entity;
                }
                catch(Exception)
                {
                    var businessLogicObject = new LinearRegression.BusinessLogic.Regression(xdata.ToList(), ydata.ToList());

                    var b0 = businessLogicObject.Parameter0;//businessLogicObject.GetRegressionEquation().firstParameter;
                    var b1 = businessLogicObject.Parameter1;//businessLogicObject.GetRegressionEquation().secondParameter;

                    var adjustedYs = businessLogicObject.GetAdjustedYsValues().ToArray();
                    this.areAdjustedYsCalculated = true;
                    //adjustedYsList = adjustedYs.ToList();

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
                
                var businessLogicObject = new LinearRegression.BusinessLogic.Regression(xdata.ToList(), ydata.ToList());

                var b0 = businessLogicObject.Parameter0;//businessLogicObject.GetRegressionEquation().firstParameter;
                var b1 = businessLogicObject.Parameter1;//businessLogicObject.GetRegressionEquation().secondParameter;

                var adjustedYs = businessLogicObject.GetAdjustedYsValues().ToArray();
                this.areAdjustedYsCalculated = true;
                //adjustedYsList = adjustedYs.ToList();


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

            

            //throw new NotImplementedException();
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
