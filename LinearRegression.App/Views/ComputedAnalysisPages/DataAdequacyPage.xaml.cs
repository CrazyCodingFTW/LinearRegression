using LinearRegression.App.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace LinearRegression.App.Views.ComputedAnalysisPages
{
    /// <summary>
    /// Interaction logic for DataAdequacyPage.xaml
    /// </summary>
    public partial class DataAdequacyPage : Page
    {
        private IFullAnalysis<IAdjustedDataRow> analysisData;

        public DataAdequacyPage(IFullAnalysis<IAdjustedDataRow> analysisData)
        {
            InitializeComponent();
            this.analysisData = analysisData;

            GenerateStrings();
        }

        private void GenerateStrings()
        {
            ZeroHypothesis.Text = $"H0: There is no statistically significant difference between the {analysisData.XMeaning} and {analysisData.YMeaning} and the model is indadequate.";
            AlternativeHypothesis.Text = $"H1: There is statistically significant difference between the {analysisData.XMeaning} and {analysisData.YMeaning} and the model is indadequate";
            AlphaLevel.Text = $"\x3b1 = 0.05";
            PreferredTest.Text = "F - test";
        }
    }
}
