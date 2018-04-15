using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinearRegression.App.Contracts
{
    public interface IAnalysisDataRow : IEditableObject
    {
        long Index { get; }

        double X { get; }
        double Y { get; }
    }
}
