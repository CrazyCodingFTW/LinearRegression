using LinearRegression.Database.ModelContracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinearRegression.Database.Model
{
    public class Comment : IComment
    {
        public Comment() { }

        public Comment(long analysisInformationId, string username, string content)
        {
            this.AnalysisInformationID = analysisInformationId;
            this.Username = username;
            this.Content = content;
        }

        public long Id { get; set; }
        public long AnalysisInformationID { get; set; }
        public string Username { get; set; }
        public string Content { get; set; }
    }
}
