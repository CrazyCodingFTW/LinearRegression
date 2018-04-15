using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinearRegression.App.Contracts
{
    interface IViewModel<TObservableItem> where TObservableItem : class
    {
        ObservableCollection<TObservableItem> Data { get; set; }

        void GenerateData<TOriginatingItem>(IEnumerable<TOriginatingItem> items, Func<TOriginatingItem, TObservableItem> converter) where TOriginatingItem : class;
    }
}
