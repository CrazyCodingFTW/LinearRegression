using LinearRegression.Database.ModelContracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinearRegression.Database.ModelAdapters
{
    public class Comment : ModelAdapter<Model.Comment>
    {
        public Comment(long analysisInformationId, string user, string content, IModelController<LinearRegressionDbContext> controller) : base(controller)
        {
            if (string.IsNullOrWhiteSpace(user) || string.IsNullOrWhiteSpace(content))
                throw new ArgumentException("All data must be filled!");

            if (analysisInformationId == 0)
                throw new ArgumentException("You must save the Analysis Information data, before assigning comments to it!");

            this.AnalysisInformationId = analysisInformationId;
            this.User = user;
            this.Content = content;
        }

        public Comment(Model.Comment commentEntity, IModelController<LinearRegressionDbContext> controller):base(commentEntity,controller)
        {
            this.AnalysisInformationId = commentEntity.AnalysisInformationID;
            this.User = commentEntity.Username;
            this.Content = commentEntity.Content;
        }

        public long AnalysisInformationId { get; protected set; }

        public string User { get; set; }

        public string Content { get; set; }

        public override void Delete(LinearRegressionDbContext db)
        {
            if (this.Id != 0)
            {
                db.CommentSet.Remove(this.Entity);
                var analysisInformation = db.AnalysisInformationSet.Find(Entity.AnalysisInformationID);
                analysisInformation.RemoveComment(this.Entity);

                db.SaveChanges();
            }

            else throw new InvalidOperationException("Unsaved data cannot be deleted.");
        }

        public override void Save(LinearRegressionDbContext db)
        {
            if (this.Entity is null)
                this.Entity = new Model.Comment(this.AnalysisInformationId, this.User, this.Content);

            else
                this.Entity.Content = this.Content;

            if (db.CommentSet.Any(c => c.Id == this.Entity.Id))
                db.CommentSet.Update(this.Entity);

            else db.CommentSet.Add(this.Entity);

            db.SaveChanges();

            //Adding the comment after saving it so the id is correct.
            var analysisInformation = db.AnalysisInformationSet.Find(this.AnalysisInformationId);
            analysisInformation.AddComment(this.Entity);
            db.AnalysisInformationSet.Update(analysisInformation);
            db.SaveChanges();

            this.Id = this.Entity.Id;
        }
    }
}
