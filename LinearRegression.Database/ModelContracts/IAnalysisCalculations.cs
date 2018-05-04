using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinearRegression.Database.ModelContracts
{
    public interface IAnalysisCalculations : IDBEntity
    {
        long Id { get; set; }
        long AnalysisDataID { get; set; }

        //TODO: Complete
    }
}
