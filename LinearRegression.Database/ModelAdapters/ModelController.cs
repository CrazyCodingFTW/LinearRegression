using LinearRegression.Database.ModelContracts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinearRegression.Database.ModelAdapters
{
    public class ModelController<TDbContext> : IModelController<TDbContext> where TDbContext : DbContext, new()
    {
        private string modelsNamespace;

        /// <summary>
        /// Creates the ModelController class which is able to comunicate with group of entities
        /// </summary>
        /// <param name="modelsNamespace">The namespace in which the models are placed</param>
        public ModelController(string modelsNamespace)
        {
            if (string.IsNullOrEmpty(modelsNamespace))
                throw new ArgumentNullException("You must provide the location of the models!");

            this.modelsNamespace = modelsNamespace;
        }


        /// <summary>
        /// Gets a list with all the entities
        /// </summary>
        /// <typeparam name="TModelAdapter">The adapter which is going to take the entity</typeparam>
        /// <returns></returns>
        public IReadOnlyCollection<TModelAdapter> GetAllEntities<TModelAdapter>() where TModelAdapter : class, IDBEntity
        {
            List<TModelAdapter> adapterEntities = new List<TModelAdapter>();

            using (var db = new TDbContext())
            {
                var set = GetEntitySet(GetEntityType(typeof(TModelAdapter)), db);

                foreach (var entity in set)
                {
                    var instance = Activator.CreateInstance(typeof(TModelAdapter), entity, this) as TModelAdapter;
                    adapterEntities.Add(instance);
                }
            }

            return adapterEntities;
        }

        /// <summary>
        ///  Gets the first entity which meats a given condition
        /// </summary>
        /// <typeparam name="TModelAdapter">The adapter which is going to take the entity</typeparam>
        /// <param name="function">Functiion to find an entity</param>
        /// <returns></returns>
        public TModelAdapter FindEntity<TModelAdapter>(Func<IDBEntity, bool> function) where TModelAdapter : class, IDBEntity
        {
            IDBEntity entity;
            using (var db = new TDbContext())
            {
                var set = GetEntitySet(GetEntityType(typeof(TModelAdapter)), db);
                entity = set.Where(function).FirstOrDefault() as IDBEntity;
            }

            var entityAdapter = Activator.CreateInstance(typeof(TModelAdapter), entity, this) as TModelAdapter;
            return entityAdapter;
        }

        /// <summary>
        /// Look into the database and find entity with the given id
        /// </summary>
        /// <typeparam name="TModelAdapter">The adapter which is going to extract the data from the entity</typeparam>
        /// <param name="id"></param>
        /// <returns></returns>
        public TModelAdapter GetEntityById<TModelAdapter>(long id) where TModelAdapter : class, IDBEntity
        {
            TModelAdapter entityInstance;
            IDBEntity foundEntity;

            using (var db = new TDbContext())
            {
                var entitySet = GetEntitySet(GetEntityType(typeof(TModelAdapter)), db);
                foundEntity = entitySet.FirstOrDefault(e => e.Id == id) as IDBEntity;
            }

            entityInstance = Activator.CreateInstance(typeof(TModelAdapter), foundEntity, this) as TModelAdapter;

            return entityInstance;
        }

        /// <summary>
        /// Receives a collection with entities and saves them effectively 
        /// </summary>
        /// <typeparam name="TModelAdapter">The adapter containing the entity</typeparam>
        /// <param name="entities">Collection with the adapters containing the entities</param>
        public void SaveAllEntities<TModelAdapter>(IReadOnlyCollection<TModelAdapter> entities) where TModelAdapter : class, IDBEntity
        {
            using (var db = new TDbContext())
            {
                foreach (var entity in entities)
                    ((ISaveRemoveable<TDbContext>)entity).Save(db);
            }
        }

        /// <summary>
        /// Receives a collection with entities and deletes them effectively 
        /// </summary>
        /// <typeparam name="TModelAdapter">The adapter containing the entity</typeparam>
        /// <param name="entities">Collection with the adapters containing the entities</param>
        public void DeleteAllEntities<TModelAdapter>(IReadOnlyCollection<TModelAdapter> entities) where TModelAdapter : class, IDBEntity
        {
            using (var db = new TDbContext())
            {
                foreach (var entity in entities)
                    ((ISaveRemoveable<TDbContext>)entity).Delete(db);
            }
        }

        /// <summary>
        /// Looks into the database cnotext and gets the related DbSet for the current adapter
        /// </summary>
        /// <param name="entityType">The type of entity whcich set is going to be looked for</param>
        /// <param name="db">The instance of the DbContext</param>
        /// <returns></returns>
        private IEnumerable<IDBEntity> GetEntitySet(Type entityType, TDbContext db)
        {
            var set = db.GetType()
           .GetProperty(entityType.Name + "Set")
           .GetValue(db);

            var enumerableSet = set as IEnumerable;

            return enumerableSet.Cast<IDBEntity>();
        }


        /// <summary>
        /// Get the type of the entity by the given class for models and the name of the adapter
        /// </summary>
        /// <param name="adapter">the type of the adapter</param>
        /// <returns></returns>
        private Type GetEntityType(Type adapter) =>
            Type.GetType($"{modelsNamespace}.{adapter.Name}");
    }
}
