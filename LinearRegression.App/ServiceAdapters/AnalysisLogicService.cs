﻿using LinearRegression.App.Contracts;
using LinearRegression.App.Contracts.Services;
using LinearRegression.App.Models;
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

        public IAnalysisData<IAdjustedDataRow> GetAdjustedData(IAnalysisData<IAnalysisDataRow> rawAnalysisModel)
        {
            //Return X,Y and Y^ - adjusted Y.

            //TODO: First check if the database contains the analysis
            var xdata = rawAnalysisModel.Data.Select(d => d.X).ToArray();
            var ydata = rawAnalysisModel.Data.Select(d => d.Y).ToArray();

            //List<double> ys = NikiService.GetAdjustedY(xdata, ydata);

            //Creating business logic object to use the business logic methods!!!
            var businessLogicObject = new LinearRegression.BusinessLogic.LinearRegression(xdata.ToList(),ydata.ToList());

            //Getting the adjusted Y values.
            var adjustedYValues = businessLogicObject.GetAdjustedYsValues();

            //Creating observable collection with Xs, Ys and adjusted Ys.
            var collection = new ObservableCollection<IAdjustedDataRow>();

            for (int i = 0; i < adjustedYValues.Count; i++)
                collection.Add(new AdjustedDataRow(i + 1, xdata[i], ydata[i], adjustedYValues[i]));

            return new FullAnalysis<IAdjustedDataRow>(12, null, null, null, null, collection);

            //TODO: use calculations to get the adjusted data table or check if it already exists in the database
            throw new NotImplementedException();
        }
    }
}
