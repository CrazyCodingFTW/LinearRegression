using LinearRegression.App.Contracts;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace LinearRegression.App.Models
{
    public class AnalysisDataRow : NotificationObject, IAnalysisDataRow
    {
        private double x;
        private double y;

        private Dictionary<string, object> storedValues;

        public AnalysisDataRow(long index, double x, double y)
        {
            this.Index = index;
            this.X = x;
            this.Y = y;
        }

        public long Index { get; set; }

        public double X
        {
            get => this.x;
            set
            {
                this.x = value;
                this.RaisePropertyChanged("X");
            }
        }

        public double Y
        {
            get => this.y;
            set
            {
                this.y = value;
                this.RaisePropertyChanged("Y");
            }
        }

        public void BeginEdit()
        {
            this.storedValues = this.BackUp();
        }

        public void CancelEdit()
        {
            if (this.storedValues == null)
                return;

            foreach (var item in this.storedValues)
            {
                var itemProperties = this.GetType().GetTypeInfo().DeclaredProperties;
                var pDesc = itemProperties.FirstOrDefault(p => p.Name == item.Key);

                if (pDesc != null)
                    pDesc.SetValue(this, item.Value);
            }
        }

        public void EndEdit()
        {
            if (this.storedValues != null)
            {
                this.storedValues.Clear();
                this.storedValues = null;
            }
        }

        protected Dictionary<string, object> BackUp()
        {
            var dict = new Dictionary<string, object>();
            var itemProperties = this.GetType().GetTypeInfo().DeclaredProperties;

            foreach (var pDescriptor in itemProperties)
            {

                if (pDescriptor.CanWrite)
                    dict.Add(pDescriptor.Name, pDescriptor.GetValue(this));
            }
            return dict;
        }
    }
}
