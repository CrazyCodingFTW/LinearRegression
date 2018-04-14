using LinearRegression.App.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinearRegression.App.StaticData
{
    public class HelpContent : IHelpContent
    {
        public HelpContent(string title, string content)
        {
            this.Title = title;
            this.Content = content;
        }

        public string Title { get; set; }
        public string Content { get; set; }
    }
}
