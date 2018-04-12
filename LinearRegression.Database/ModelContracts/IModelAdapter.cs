using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

//The adapter is used for easier communication from the upper level to the lower level by abstracting all the database connections from the user
//And leaving just some basic functions to work with
namespace LinearRegression.Database.ModelContracts
{
    public interface IModelAdapter<TDbEntity, TDbContext> : IDBEntity, ISaveRemoveable<TDbContext> where TDbEntity : class, IDBEntity where TDbContext : DbContext
    {
        TDbEntity Entity { get; }
    }
}
