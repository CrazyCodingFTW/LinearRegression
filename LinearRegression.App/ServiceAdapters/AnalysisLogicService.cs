using LinearRegression.App.Contracts;
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
            //var xdata = rawAnalysisModel.Data.Select(d => d.X).ToArray();
            //var ydata = rawAnalysisModel.Data.Select(d => d.Y).ToArray();

            //List<double> ys = NikiService.GetAdjustedY(xdata, ydata);

            //var collection = new ObservableCollection<IAdjustedDataRow>();

            //for (int i = 0; i < ys.Count; i++)
            //    collection.Add(new AdjustedDataRow(i + 1, xdata[i], ydata[i], ys[i]));

            //return new FullAnalysis<IAdjustedDataRow>(12, null, null, null, null, collection);

            //TODO: use calculations to get the adjusted data table or check if it already exists in the database
            throw new NotImplementedException();
        }
    }
}
