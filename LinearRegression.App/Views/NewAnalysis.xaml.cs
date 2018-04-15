using LinearRegression.App.Contracts;
using LinearRegression.App.Models;
using Syncfusion.UI.Xaml.Grid;
using Syncfusion.UI.Xaml.Grid.Helpers;
using Syncfusion.UI.Xaml.ScrollAxis;
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
using System.Windows.Threading;

namespace LinearRegression.App.Views
{
    /// <summary>
    /// Interaction logic for NewAnalysis.xaml
    /// </summary>
    public partial class NewAnalysis : Page, ICustomPage
    {
        private IViewModel<AnalysisDataRow> analysisViewModel;

        public NewAnalysis()
        {
            InitializeComponent();

            analysisViewModel = new ViewModel<AnalysisDataRow>();

            DataGrid.ItemsSource = analysisViewModel.Data;
            DataChart.ItemsSource = analysisViewModel.Data;
        }

        public string PageTitle => "New Analysis";

        public IHelpContent HelpContent =>
            new HelpContent(this.PageTitle, 
                $"In this page you are able to create your Linear Rehression analysis. Fill in the forms and your X and Y data by clicking the add new row line. After you're done click 'Compute', to save and compute your data.");

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            var homeView = new HomeView();
            NavigationService.Navigate(homeView);

            DataGrid.Dispose();
        }

        private void DataGrid_AddNewRowInitiating(object sender, AddNewRowInitiatingEventArgs e)
        {
            var lastAnalysisDataIndex = this.analysisViewModel.Data.Any() ? this.analysisViewModel.Data.Last().Index + 1 : 1;

            var newObject = new AnalysisDataRow(lastAnalysisDataIndex, 0, 0);
            this.analysisViewModel.Data.Add(newObject);
        }
    }
}
