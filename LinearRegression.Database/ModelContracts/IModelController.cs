using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinearRegression.Database.ModelContracts
{
    public interface IModelController<TDbContext> where TDbContext : DbContext, new()
    {
        IReadOnlyCollection<TModelAdapter> GetAllEntities<TModelAdapter>() where TModelAdapter : class, IDBEntity;

        TModelAdapter GetEntityById<TModelAdapter>(long id) where TModelAdapter : class, IDBEntity;

        TModelAdapter FindEntity<TModelAdapter>(Func<IDBEntity, bool> function) where TModelAdapter : class, IDBEntity;

        void SaveAllEntities<TModelAdapter>(IReadOnlyCollection<TModelAdapter> entities) where TModelAdapter : class, IDBEntity;

        void DeleteAllEntities<TModelAdapter>(IReadOnlyCollection<TModelAdapter> entities) where TModelAdapter : class, IDBEntity;
    }
}
