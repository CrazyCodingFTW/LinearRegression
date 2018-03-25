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
        public TDbEntity Entity { get; protected set; }

        public abstract void Delete();
        public abstract void Save();

        /// <summary>
        /// Gets a list with all the entities
        /// </summary>
        /// <typeparam name="TModelAdapter">The adapter which will take the entity</typeparam>
        /// <returns></returns>
        public static IReadOnlyCollection<IModelAdapter<TDbEntity>> GetAllEntities<TModelAdapter>() where TModelAdapter : class, IModelAdapter<TDbEntity>
        {

            List<IModelAdapter<TDbEntity>> adapterEntities = new List<IModelAdapter<TDbEntity>>();

            using (var db = new LinearRegressionDbContext())
            {
                var set = GetEntitySet(db);
                foreach (var entity in set)
                {
                    var instance = Activator.CreateInstance(typeof(TModelAdapter), entity) as IModelAdapter<TDbEntity>;
                    adapterEntities.Add(instance);
                }
            }

            return adapterEntities;

        }

        /// <summary>
        /// Look into the database and find entity with the given id
        /// </summary>
        /// <typeparam name="TModelAdapterEntity">The adapter which is going to extract the data from the entity</typeparam>
        /// <param name="id">The Id of the searched entity</param>
        /// <returns></returns>
        public static TModelAdapterEntity GetEntityById<TModelAdapterEntity>(long id) where TModelAdapterEntity : class, IModelAdapter<TDbEntity>
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
        /// Gets the first entity which meats a given condition
        /// </summary>
        /// <typeparam name="TModelAdapterEntity">The name of the adapter class which relates with the entity</typeparam>
        /// <param name="function">Functiion to find an entity</param>
        /// <returns></returns>
        public static TModelAdapterEntity FindEntity<TModelAdapterEntity>(Func<TDbEntity, bool> function) where TModelAdapterEntity : class, IModelAdapter<TDbEntity>
        {
            TDbEntity entity;
            using (var db = new LinearRegressionDbContext())
            {
                var set = GetEntitySet(db);
                entity = set.Where(function).FirstOrDefault();
            }

            var entityAdapter = Activator.CreateInstance(typeof(TModelAdapterEntity), entity) as TModelAdapterEntity;
            return entityAdapter;
        }

        /// <summary>
        /// Look into the database and find entity with the given id. The method is suitable when looking for foreign to this adapter entities
        /// </summary>
        /// <typeparam name="TModelAdapterEntity">The adapter which is going to extract the data from the entity</typeparam>
        /// <typeparam name="TReqDbEntity">The Model class of entity itself</typeparam>
        /// <param name="id">The Id of the searched entity</param>
        /// <returns></returns>
        internal static TModelAdapterEntity GetEntityById<TModelAdapterEntity, TReqDbEntity>(long id) where TModelAdapterEntity : class, IModelAdapter<TReqDbEntity> where TReqDbEntity : class, IDBEntity
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
        internal static DbSet<ReqEntity> GetEntitySet<ReqEntity>(LinearRegressionDbContext db) where ReqEntity : class, IDBEntity
        {
            var set = db.GetType()
           .GetProperty(typeof(ReqEntity).Name + "Set")
           .GetValue(db);

            return set as DbSet<ReqEntity>;
        }

    }
}
