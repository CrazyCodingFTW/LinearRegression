
using System.Collections.Generic;

namespace LinearRegression.App.Contracts.Services
{
    public interface IAnalysisLogicService:IService
    {
        IAnalysisData<IAdjustedDataRow> GetAdjustedData(IAnalysisData<IAnalysisDataRow> rawAnalysisModel);
    }
}
