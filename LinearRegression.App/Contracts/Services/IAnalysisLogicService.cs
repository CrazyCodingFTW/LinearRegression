﻿
using System.Collections.Generic;

namespace LinearRegression.App.Contracts.Services
{
    public interface IAnalysisLogicService:IService
    {
        IAnalysisData<IAdjustedDataRow> GetAdjustedData(IAnalysisData<IAnalysisDataRow> rawAnalysisModel);
        IFullAnalysis<IAdjustedDataRow> GetFullAnalysisAdjustedData(IFullAnalysis<IAnalysisDataRow>  fullRawAnalysisModel);
    }
}
