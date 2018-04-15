using LinearRegression.App.Contracts;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinearRegression.App.Models
{
    public class ViewModel<TObservableItem> : IViewModel<TObservableItem> where TObservableItem : class
    {
        public ViewModel()
        {
            this.Data = new ObservableCollection<TObservableItem>();
        }

        public ObservableCollection<TObservableItem> Data { get; set; }

        public void GenerateData<TOriginatingItem>(IEnumerable<TOriginatingItem> items, Func<TOriginatingItem, TObservableItem> converter) where TOriginatingItem : class
        {
            foreach (var item in items)
                this.Data.Add(converter(item));
        }
    }
}
