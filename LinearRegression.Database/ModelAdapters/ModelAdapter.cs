using LinearRegression.Database.ModelContracts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinearRegression.Database.ModelAdapters
{
    public abstract class ModelAdapter<TDbEntity> : IModelAdapter<TDbEntity,LinearRegressionDbContext> where TDbEntity : class, IDBEntity
    {
        public long Id { get; set; }

        /// <summary>
        /// Converts the ModelAdapter data to the related entity object
        /// </summary>
        public TDbEntity Entity { get; protected set; }

        public abstract void Delete(LinearRegressionDbContext db);
        public virtual void Delete()
        {
            using (var db = new LinearRegressionDbContext())
                this.Delete(db);
        }

        public abstract void Save(LinearRegressionDbContext db);
        public virtual void Save()
        {
            using (var db = new LinearRegressionDbContext())
                this.Save(db);
        }
    }
}
