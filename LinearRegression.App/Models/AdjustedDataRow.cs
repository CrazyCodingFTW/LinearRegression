using LinearRegression.App.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinearRegression.App.Models
{
    public class AdjustedDataRow : AnalysisDataRow, IAdjustedDataRow
    {
        private double adjustedY;

        public AdjustedDataRow(long index, double x, double y, double adjustedY) : base(index, x, y)
        {
            this.AdjustedY = adjustedY;
        }

        public double AdjustedY
        {
            get => this.adjustedY;
            set
            {
                this.adjustedY = value;
                this.RaisePropertyChanged("AdjustedY");
            }
        }
    }
}
