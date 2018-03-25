using System;
using System.Collections.Generic;
using System.Text;

namespace LinearRegression.Database.ModelContracts
{
    public interface IDBEntity
    {
        long Id { get; set; }
    }
}
