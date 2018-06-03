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
    /// Interaction logic for AverageAndMaximalErrorsPage.xaml
    /// </summary>
    public partial class AverageAndMaximalErrorsPage : Page
    {
        private IAnalysisCalculations calculations;

        public AverageAndMaximalErrorsPage(IAnalysisCalculations calculations)
        {
            InitializeComponent();
            this.calculations = calculations;

            ShowsData();
        }

        private void ShowsData()
        {
            AverageErrorB0Output.Text += calculations.AverageErrorB0.ToString("F3");
            AverageErrorB1Output.Text += calculations.AverageErrorB1.ToString("F3");

            MaximalErrorB0Output.Text += calculations.MaximalErrorB0.ToString("F3");
            MaximalErrorB1Output.Text += calculations.MaximalErrorB1.ToString("F3");
        }
    }
}
