using LinearRegression.Contracts.ModelContracts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinearRegression.Database.ModelAdapters
{
    public abstract class ModelAdapter<TDbEntity> : IModelAdapter<TDbEntity> where TDbEntity : class, IDBEntity
    {
        public long Id { get; set; }

        /// <summary>
        /// Converts the ModelAdapter data to the related entity object
        /// </summary>
        internal abstract TDbEntity Entity { get; set; }

        public void Delete()
        {
            using (var db = new LinearRegressionDbContext())
            {
                OnDelete();
                var entitySet = GetEntitySet(db);
                entitySet.Remove(Entity);

                db.SaveChanges();
            }
        }

        /// <summary>
        /// Adds or updates an entity
        /// </summary>
        public void Save()
        {
            using (var db = new LinearRegressionDbContext())
            {
                OnSave();
                var entitySet = GetEntitySet(db);

                if (entitySet.Any(e => e.Id == Entity.Id))
                {
                    var entity = entitySet.Find(this.Entity.Id);
                    entitySet.Remove(entity);
                    db.SaveChanges();
                }

                entitySet.Add(Entity);


                db.SaveChanges();
            }

            this.Id = Entity.Id;
        }

        /// <summary>
        /// Look into the database and find entity with the given id
        /// </summary>
        /// <typeparam name="TModelAdapterEntity">The adapter which is going to extract the data from the entity</typeparam>
        /// <param name="id">The Id of the searched entity</param>
        /// <returns></returns>
        public static TModelAdapterEntity GetEntity<TModelAdapterEntity>(long id) where TModelAdapterEntity : class, IModelAdapter<TDbEntity>
        {
            TModelAdapterEntity entityInstance;
            TDbEntity foundEntity;

            using (var db = new LinearRegressionDbContext())
            {
                var entitySet = GetEntitySet(db);
                foundEntity = entitySet.Find(id) as TDbEntity;
            }

            entityInstance = Activator.CreateInstance(typeof(TModelAdapterEntity), foundEntity) as TModelAdapterEntity;

            return entityInstance;
        }

        /// <summary>
        /// Look into the database and find entity with the given id. The method is suitable when looking for foreign to this adapter entities
        /// </summary>
        /// <typeparam name="TModelAdapterEntity">The adapter which is going to extract the data from the entity</typeparam>
        /// <typeparam name="TReqDbEntity">The Model class of entity itself</typeparam>
        /// <param name="id">The Id of the searched entity</param>
        /// <returns></returns>
        internal static TModelAdapterEntity GetEntity<TModelAdapterEntity, TReqDbEntity>(long id) where TModelAdapterEntity : class, IModelAdapter<TReqDbEntity> where TReqDbEntity : class, IDBEntity
        {
            TModelAdapterEntity entityInstance;
            TReqDbEntity foundEntity;
            using (var db = new LinearRegressionDbContext())
            {
                var entitySet = GetEntitySet<TReqDbEntity>(db);
                foundEntity = entitySet.Find(id) as TReqDbEntity;
            }

            entityInstance = Activator.CreateInstance(typeof(TModelAdapterEntity), foundEntity) as TModelAdapterEntity;

            return entityInstance;
        }

        /// <summary>
        /// Looks into the database cnotext and gets the related DbSet for the current adapter
        /// </summary>
        /// <param name="db"></param>
        /// <returns></returns>
        internal static DbSet<TDbEntity> GetEntitySet(LinearRegressionDbContext db)
        {
            var set = db.GetType()
           .GetProperty(typeof(TDbEntity).Name + "Set")
           .GetValue(db);

            return set as DbSet<TDbEntity>;
        }

        /// <summary>
        /// Looks into the database cnotext and gets the related DbSet for a foreign adapter
        /// </summary>
        /// <typeparam name="ReqEntity">The required Model for which set it is looked for</typeparam>
        /// <param name="db"></param>
        /// <returns></returns>
        internal static DbSet<ReqEntity> GetEntitySet<ReqEntity>(LinearRegressionDbContext db) where ReqEntity:class, IDBEntity
        {
            var set = db.GetType()
           .GetProperty(typeof(ReqEntity).Name + "Set")
           .GetValue(db);

            return set as DbSet<ReqEntity>;
        }


        protected abstract void OnSave();
        protected abstract void OnDelete();

    }
}
