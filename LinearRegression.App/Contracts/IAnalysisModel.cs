using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinearRegression.App.Contracts
{
    public interface IAnalysisModel
    {
        string Title { get; }

        string Description { get; }

        string XMeaning { get; }

        string YMeaning { get; }

        ObservableCollection<IAnalysisDataRow> Data { get; }

        DateTime CreationDate { get; }
    }
}
