using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinearRegression.App.Contracts
{
    public interface IAdjustedDataRow : IAnalysisDataRow
    {
        double AdjustedY { get; }
    }
}
