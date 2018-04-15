using LinearRegression.App.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinearRegression.App.Models
{
    public class AnalysisDataRow : IAnalysisDataRow
    {
        public AnalysisDataRow(long index, double x, double y)
        {
            this.Index = index;
            this.X = x;
            this.Y = y;
        }

        public long Index { get; set; }

        public double X { get; set; }

        public double Y { get; set; }
    }
}
