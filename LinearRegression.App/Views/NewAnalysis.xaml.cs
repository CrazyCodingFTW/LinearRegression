using LinearRegression.App.Contracts;
using LinearRegression.App.CustomControls;
using LinearRegression.App.Models;
using MaterialDesignThemes.Wpf;
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
    public partial class NewAnalysis : Page, ICustomPage
    {
        private IViewModel<AnalysisDataRow> analysisViewModel;
        private CustomXYMeaningPrompt xyMeaningPrompt;

        private string xHeader = "X";
        private string yHeader = "Y";

        public NewAnalysis()
        {
            InitializeComponent();

            analysisViewModel = new ViewModel<AnalysisDataRow>();

            DataGrid.ItemsSource = analysisViewModel.Data;
            DataChart.ItemsSource = analysisViewModel.Data;
        }

        public string XHeader
        {
            get => this.xHeader;
            private set
            {
                this.xHeader = value;
                XColumn.HeaderText = xyMeaningPrompt.EnteredText;
                XAxis.Header = xyMeaningPrompt.EnteredText;
            }
        }

        public string YHeader
        {
            get => this.yHeader;
            private set
            {
                this.yHeader = value;
                YColumn.HeaderText = xyMeaningPrompt.EnteredText;
                YAxis.Header = xyMeaningPrompt.EnteredText;
            }
        }

        public string PageTitle => "New Analysis";

        public IHelpContent HelpContent =>
            new HelpContent(this.PageTitle,
                $"In this page you are able to create your Linear Rehression analysis. Fill in the forms and your X and Y data by clicking the add new row line. After you're done click 'Compute', to save and compute your data." +
                $"{Environment.NewLine}To change the meanings of your X/Y data, click the \"Change X/Y\" buttons.");

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

        private async void ChangeX_Click(object sender, RoutedEventArgs e)
        {
            this.xyMeaningPrompt = new CustomXYMeaningPrompt(CustomXYMeaningPrompt.Variables.X);

            //Await waits for the dialog to close. Otherwise the methods after will execute before the dialog closing, resulting in unchanged data
            await DialogHost.Show(this.xyMeaningPrompt);

            if (this.xyMeaningPrompt.ClosedWithConfirmation)
                this.XHeader = xyMeaningPrompt.EnteredText;

            xyMeaningPrompt = null;
        }

        private async void ChangeY_Click(object sender, RoutedEventArgs e)
        {
            this.xyMeaningPrompt = new CustomXYMeaningPrompt(CustomXYMeaningPrompt.Variables.Y);

            //Await waits for the dialog to close. Otherwise the methods after will execute before the dialog closing, resulting in unchanged data
            await DialogHost.Show(this.xyMeaningPrompt);

            if (this.xyMeaningPrompt.ClosedWithConfirmation)
                this.YHeader = xyMeaningPrompt.EnteredText;

            xyMeaningPrompt = null;
        }
    }
}
