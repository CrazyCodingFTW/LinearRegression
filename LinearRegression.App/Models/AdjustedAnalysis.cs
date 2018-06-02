using LinearRegression.App.Contracts;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinearRegression.App.Models
{
    public class AdjustedAnalysis : IAnalysisData<IAdjustedDataRow>
    {
        public AdjustedAnalysis(string xMeaning, string yMeaning, ObservableCollection<IAdjustedDataRow> data)
        {
            this.XMeaning = xMeaning;
            this.YMeaning = yMeaning;
            this.Data = data;
        }

        public string XMeaning { get; private set; }

        public string YMeaning { get; private set; }

        public ObservableCollection<IAdjustedDataRow> Data { get; private set; }
    }
}
