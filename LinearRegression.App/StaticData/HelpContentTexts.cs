using LinearRegression.App.Contracts;
using LinearRegression.App.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace LinearRegression.App.StaticData
{
    public class HelpContentTexts
    {
        private static readonly Dictionary<Type, IHelpContent> helpContents = new Dictionary<Type, IHelpContent>
        {
            { typeof(HomeView), new HelpContent("Home",$"In the home view you have three options to choose from:{Environment.NewLine}{Environment.NewLine}New Analysis - creates new Linear Regression analysis{Environment.NewLine}{Environment.NewLine}History - browse previous analysis{Environment.NewLine}{Environment.NewLine}Export Data - Choose analysis to export into Excel")}
        };

        public static IHelpContent GetHelpContent(Type pageType) =>
            helpContents[pageType];

    }
}
