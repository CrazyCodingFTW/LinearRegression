using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinearRegression.App.Contracts.Services
{
    public interface IDatabaseService : IService
    {
        /// <summary>
        /// Takes a newly made entity and saves its thata to the database. It also sets the id given by the database
        /// </summary>
        /// <param name="analysisModel">The newly created entity</param>
        /// <returns>The id given from the database</returns>
        long SaveEntity(IFullAnalysis<IAnalysisDataRow> analysisModel);
    }
}
