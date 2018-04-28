using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinearRegression.App.Contracts
{
    public interface IFullAnalysis : IAnalysisMetadata
    {
        string XMeaning { get; }

        string YMeaning { get; }

        ObservableCollection<IAnalysisDataRow> Data { get; }
    }
}
