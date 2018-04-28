using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinearRegression.App.Contracts
{
    public interface IAnalysisMetadata
    {
        string Title { get; }

        string Description { get; }

        DateTime CreationDate { get; }
    }
}
