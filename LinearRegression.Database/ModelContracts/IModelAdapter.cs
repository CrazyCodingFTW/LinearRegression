using System;
using System.Collections.Generic;
using System.Text;

namespace LinearRegression.Database.ModelContracts
{
    public interface IModelAdapter<TDbEntity> : IDBEntity where TDbEntity : class, IDBEntity
    {
        TDbEntity Entity { get; }
        void Save();
        void Delete();
    }
}
