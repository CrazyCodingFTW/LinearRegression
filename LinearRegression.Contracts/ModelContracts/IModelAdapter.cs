using System;
using System.Collections.Generic;
using System.Text;

namespace LinearRegression.Contracts.ModelContracts
{
    public interface IModelAdapter<TDbEntity> : IDBEntity where TDbEntity : class, IDBEntity
    {
        void Save();
        void Delete();
    }
}
