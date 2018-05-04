using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LinearRegression.Database.ModelContracts;
using LinearRegression.Database.Model;

namespace LinearRegression.Database.ModelAdapters
{
    public class AnalysisInformation : ModelAdapter<Model.AnalysisInformation>
    {
        private AnalysisData data;

        public AnalysisInformation(DateTime creationDate, string title, string description, IModelController<LinearRegressionDbContext> controller) : base(controller)
        {
            this.CreationDate = creationDate;
            this.Title = title;
            this.Descrioption = description;
        }

        public AnalysisInformation(Model.AnalysisInformation ai, IModelController<LinearRegressionDbContext> controller) : base(ai, controller)
        {
            this.CreationDate = ai.GetDateTimeFromString();
            this.Title = ai.Title;
            this.Descrioption = ai.Descrioption;
        }

        public AnalysisData Data
        {
            get
            {
                if (this.data is null)
                    this.data = this.Controller.GetEntityById<AnalysisData>(Entity.AnalysisDataId);

                return this.data;
            }

            set => this.data = value;
        }

        public DateTime CreationDate { get; set; }

        public string Title { get; set; }

        public string Descrioption { get; set; }

        public IReadOnlyCollection<Comment> Comments
        {
            get
            {
                if (this.Id == 0)
                    throw new InvalidOperationException("You must save the entity before working with its comments");

                else return this.Controller.GetAllEntities<Comment>().Where(c => c.AnalysisInformationId == this.Id).ToList();
            }
        }

        public override void Save(LinearRegressionDbContext db)
        {
            //If the entity is null, it means that there is no record of the entity (It is newly created)
            if (Entity is null)
                this.Entity = new Model.AnalysisInformation(this.CreationDate, this.Title, this.Descrioption);

            //If the entity has value, it will only be updated
            else
            {
                this.Entity.AnalysisDataId = this.Data.Id;
                Entity.ConvertDatetimeToString(this.CreationDate);
                Entity.Title = this.Title;
                Entity.Descrioption = this.Descrioption;
            }

            if (db.AnalysisInformationSet.Any(d => d.Id == Entity.Id))
                db.AnalysisInformationSet.Update(this.Entity);

            else db.AnalysisInformationSet.Add(this.Entity);

            db.SaveChanges();

            //Update the id of the current instance to keep the relation
            this.Id = Entity.Id;

        }

        /// <summary>
        /// Deletes the information and the data related with it
        /// </summary>
        public override void Delete(LinearRegressionDbContext db)
        {
            var dataEntity = this.Data.Entity;
            db.AnalysisDataSet.Remove(dataEntity);
            db.AnalysisInformationSet.Remove(this.Entity);

            db.CommentSet.RemoveRange(this.Comments.Select(c => c.Entity).ToArray());

            db.SaveChanges();

            this.Data = null;
        }
    }
}
