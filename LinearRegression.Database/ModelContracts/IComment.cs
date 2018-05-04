using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinearRegression.Database.ModelContracts
{
    public interface IComment : IDBEntity
    {
        long AnalysisInformationID { get; set; }

        string Username { get; set; }
        string Content { get; set; }
    }
}
