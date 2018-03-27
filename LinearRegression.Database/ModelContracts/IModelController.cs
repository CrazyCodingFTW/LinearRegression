using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinearRegression.Database.ModelContracts
{
    public interface IModelController<TDbContext> where TDbContext:DbContext
    {
        IReadOnlyCollection<TModelAdapter> GetAllEntities<TModelAdapter, TDbEntity>() where TModelAdapter : class, IModelAdapter<TDbEntity, TDbContext> where TDbEntity : class, IDBEntity;

        TModelAdapter GetEntityById<TModelAdapter, TDbEntity>(long id) where TModelAdapter:class, IModelAdapter<TDbEntity, TDbContext> where TDbEntity : class, IDBEntity;

        TModelAdapter FindEntity<TModelAdapter, TDbEntity>(Func<TDbEntity, bool> function) where TModelAdapter : class, IModelAdapter<TDbEntity, TDbContext> where TDbEntity : class, IDBEntity;

        void SaveAllEntities<TModelAdapter, TDbEntity>(IReadOnlyCollection<TModelAdapter> entities) where TModelAdapter : class, IModelAdapter<TDbEntity, TDbContext> where TDbEntity:class,IDBEntity;

        void DeleteAllEntities<TModelAdapter, TDbEntity>(IReadOnlyCollection<TModelAdapter> entities) where TModelAdapter : class, IModelAdapter<TDbEntity, TDbContext> where TDbEntity : class, IDBEntity;
    }
}
