using LinearRegression.Database.ModelContracts;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinearRegression.Database.Model
{
    public class AnalysisInformation : IAnalysisInformation
    {
        public AnalysisInformation() { }

        public AnalysisInformation(DateTime creationDate, string title, string description)
        {
            ConvertDatetimeToString(creationDate);
            this.Title = title;
            this.Descrioption = description;
            this.CommentIDs = string.Empty;
        }

        public long Id { get; set; }
        public long AnalysisDataId { get; set; }
        public string CreationDate { get; set; }
        public string Title { get; set; }
        public string Descrioption { get; set; }
        public string CommentIDs { get; set; }

        public DateTime GetDateTimeFromString()
        {
            var date = DateTime.ParseExact(this.CreationDate, "yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture);
            return date;
        }

        public void ConvertDatetimeToString(DateTime date)
        {
            this.CreationDate = date.ToString("yyyy-MM-dd HH:mm:ss");
        }

        public long[] GetCommentIds()
        {
            var commentIds = this.CommentIDs.Split(" ".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);

            return commentIds.Any() ? commentIds.Select(long.Parse).ToArray() : new long[0];
        }

        public void AddComment(IComment comment)
        {
            if (!this.GetCommentIds().Contains(comment.Id))
                this.CommentIDs += $"{this.CommentIDs} {comment.Id}";
        }

        public void RemoveComment(IComment comment)
        {
            var commentIds = GetCommentIds().Where(id => id != comment.Id);
            this.CommentIDs = string.Join(" ", commentIds);
        }
    }
}
