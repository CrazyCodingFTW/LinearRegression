using LinearRegression.Database.ModelContracts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinearRegression.Database.ModelAdapters
{
    public class ModelController : IModelController<LinearRegressionDbContext>
    {
        /// <summary>
        /// Gets a list with all the entities
        /// </summary>
        /// <typeparam name="TModelAdapter">The adapter which is going to take the entity</typeparam>
        /// <typeparam name="TDbEntity">The entity which is given to its related adapter</typeparam>
        /// <returns></returns>
        IReadOnlyCollection<TModelAdapter> IModelController<LinearRegressionDbContext>.GetAllEntities<TModelAdapter, TDbEntity>()
        {
            List<TModelAdapter> adapterEntities = new List<TModelAdapter>();

            using (var db = new LinearRegressionDbContext())
            {
                var set = GetEntitySet<TDbEntity>(db);
                foreach (var entity in set)
                {
                    var instance = Activator.CreateInstance(typeof(TModelAdapter), entity) as TModelAdapter;
                    adapterEntities.Add(instance);
                }
            }

            return adapterEntities;
        }

        /// <summary>
        ///  Gets the first entity which meats a given condition
        /// </summary>
        /// <typeparam name="TModelAdapter">The adapter which is going to take the entity</typeparam>
        /// <typeparam name="TDbEntity">The entity which is given to its related adapter</typeparam>
        /// <param name="function">Functiion to find an entity</param>
        /// <returns></returns>
        TModelAdapter IModelController<LinearRegressionDbContext>.FindEntity<TModelAdapter, TDbEntity>(Func<TDbEntity, bool> function)
        {
            TDbEntity entity;
            using (var db = new LinearRegressionDbContext())
            {
                var set = GetEntitySet<TDbEntity>(db);
                entity = set.Where(function).FirstOrDefault() as TDbEntity;
            }

            var entityAdapter = Activator.CreateInstance(typeof(TModelAdapter), entity) as TModelAdapter;
            return entityAdapter;
        }

        /// <summary>
        /// Look into the database and find entity with the given id
        /// </summary>
        /// <typeparam name="TModelAdapter">The adapter which is going to extract the data from the entity</typeparam>
        /// <typeparam name="TDbEntity">The entity which is going to be given to the adapter</typeparam>
        /// <param name="id"></param>
        /// <returns></returns>
        TModelAdapter IModelController<LinearRegressionDbContext>.GetEntityById<TModelAdapter, TDbEntity>(long id)
        {
            TModelAdapter entityInstance;
            TDbEntity foundEntity;

            using (var db = new LinearRegressionDbContext())
            {
                var entitySet = GetEntitySet<TDbEntity>(db);
                foundEntity = entitySet.Find(id) as TDbEntity;
            }

            entityInstance = Activator.CreateInstance(typeof(TModelAdapter), foundEntity) as TModelAdapter;

            return entityInstance;
        }

        /// <summary>
        /// Looks into the database cnotext and gets the related DbSet for the current adapter
        /// </summary>
        /// <param name="db"></param>
        /// <returns></returns>
        private static DbSet<TDbEntity> GetEntitySet<TDbEntity>(LinearRegressionDbContext db) where TDbEntity:class, IDBEntity
        {
            var set = db.GetType()
           .GetProperty(typeof(TDbEntity).Name + "Set")
           .GetValue(db);

            return set as DbSet<TDbEntity>;
        }

        /// <summary>
        /// Receives a collection with entities and saves them effectively 
        /// </summary>
        /// <typeparam name="TModelAdapter">The adapter containing the entity</typeparam>
        /// <typeparam name="TDbEntity">The entity itself</typeparam>
        /// <param name="entities">Collection with the adapters containing the entities</param>
        void IModelController<LinearRegressionDbContext>.SaveAllEntities<TModelAdapter, TDbEntity>(IReadOnlyCollection<TModelAdapter> entities)
        {
            using (var db = new LinearRegressionDbContext())
            {
                foreach (var entity in entities)
                    entity.Save(db);
            }
        }

        /// <summary>
        /// Receives a collection with entities and deletes them effectively 
        /// </summary>
        /// <typeparam name="TModelAdapter">The adapter containing the entity</typeparam>
        /// <typeparam name="TDbEntity">The entity itself</typeparam>
        /// <param name="entities">Collection with the adapters containing the entities</param>
        void IModelController<LinearRegressionDbContext>.DeleteAllEntities<TModelAdapter, TDbEntity>(IReadOnlyCollection<TModelAdapter> entities)
        {
            using (var db = new LinearRegressionDbContext())
            {
                foreach (var entity in entities)
                    entity.Delete(db);
            }
        }
    }
}
