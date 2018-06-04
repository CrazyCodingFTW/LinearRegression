using LinearRegression.App.Contracts;
using LinearRegression.Database.ModelContracts;
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
        private IAnalysisCalculations calculations;

        public DataAdequacyPage(IFullAnalysis<IAdjustedDataRow> analysisData, IAnalysisCalculations calculations)
        {
            InitializeComponent();
            this.analysisData = analysisData;
            this.calculations = calculations;

            GenerateStrings();
        }

        private void GenerateStrings()
        {
            //1.
            ZeroHypothesis.Text = $"H0: There is no statistically significant difference between the {analysisData.XMeaning} and {analysisData.YMeaning} and the model is indadequate.";
            AlternativeHypothesis.Text = $"H1: There is statistically significant difference between the {analysisData.XMeaning} and {analysisData.YMeaning} and the model is indadequate";
            
            //2.
            AlphaLevel.Text = $"\x3b1 = 0.05";

            //3.
            PreferredTest.Text = "F - test";

            //4.
            B0Output.Text = $"B0: {calculations.B0:F3}";
            B1Output.Text = $"B1: {calculations.B1:F3}";

            ExplainedDispersionOutput.Text = $"Explained dispersion: {calculations.ExplainedDispersion:F3}";
            ResidualDispersionOutput.Text = $"Residual dispersion: {calculations.ResidualDispersion:F3}";

            NumberOfUnits.Text = $"Number of units (n): {analysisData.Data.Count}";
            NumberOfParameters.Text = "Number of parameters: 2";

            //5.
            FResult.Text = $"F: {calculations.FEmpirical:F3}";

            //7.
            TheoreticalFOutput.Text = $"Ft: {calculations.FTheoretical:F3}";

            //8.
            ConclusionOutput.Text = $"Acording to the analysis the model is {(calculations.FEmpirical > calculations.FTheoretical ? "adequate" : "inadequate")}.";
        }
    }
}
