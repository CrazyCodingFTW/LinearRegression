using LinearRegression.App.Contracts;
using LinearRegression.App.CustomControls;
using LinearRegression.App.Models;
using MaterialDesignThemes.Wpf;
using Syncfusion.UI.Xaml.Grid;
using Syncfusion.UI.Xaml.Grid.Helpers;
using Syncfusion.UI.Xaml.ScrollAxis;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading;
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

        public NewAnalysis(IServiceProvider services)
        {
            InitializeComponent();

            this.Services = services;

            analysisViewModel = new ViewModel<AnalysisDataRow>();

            DataGrid.ItemsSource = analysisViewModel.Data;
            DataChart.ItemsSource = analysisViewModel.Data;
        }

        public string XHeader
        {
            get => this.xHeader;
            private set
            {
                if (string.IsNullOrWhiteSpace(value))
                    value = "X";

                this.xHeader = value;
                XColumn.HeaderText = value == "X" ? value : value + " (X)";
                XAxis.Header = value == "X" ? value : value + " (X)";
            }
        }

        public string YHeader
        {
            get => this.yHeader;
            private set
            {
                if (string.IsNullOrWhiteSpace(value))
                    value = "Y";

                this.yHeader = value;
                YColumn.HeaderText = value == "Y" ? value : value + " (Y)";
                YAxis.Header = value == "Y" ? value : value + " (Y)";
            }
        }

        public string PageTitle => "New Analysis";

        public IHelpContent HelpContent =>
            new HelpContent(this.PageTitle,
                $"In this page you are able to create your Linear Rehression analysis. Fill in the forms and your X and Y data by clicking the add new row line. After you're done click 'Compute', to save and compute your data." +
                $"{Environment.NewLine}To change the meanings of your X/Y data, click the \"Change X/Y\" buttons.");

        public IServiceProvider Services { get; }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            var homeView = new HomeView(this.Services);
            NavigationService.Navigate(homeView);

            DataGrid.Dispose();
        }

        private void DataGrid_AddNewRowInitiating(object sender, AddNewRowInitiatingEventArgs e)
        {
            var lastAnalysisDataIndex = this.analysisViewModel.Data.Any() ? this.analysisViewModel.Data.Last().Index + 1 : 1;

            var newObject = new AnalysisDataRow(lastAnalysisDataIndex, 0, 0);
            this.analysisViewModel.Data.Add(newObject);
        }

        private async void ChangeBtn_Click(object btn, RoutedEventArgs e)
        {
            if (btn == ChangeXBtn)
                this.xyMeaningPrompt = new CustomXYMeaningPrompt(CustomXYMeaningPrompt.Variables.X);

            else if (btn == ChangeYBtn)
                this.xyMeaningPrompt = new CustomXYMeaningPrompt(CustomXYMeaningPrompt.Variables.Y);

            //Await waits for the dialog to close. Otherwise the methods after will execute before the dialog closing, resulting in unchanged data
            await DialogHost.Show(this.xyMeaningPrompt);

            if (this.xyMeaningPrompt.ClosedWithConfirmation)
            {
                if (btn == ChangeXBtn)
                    this.XHeader = xyMeaningPrompt.EnteredText;

                else if (btn == ChangeYBtn)
                    this.YHeader = xyMeaningPrompt.EnteredText;
            }

            xyMeaningPrompt = null;
        }

        private void ComputeBtn_Click(object sender, RoutedEventArgs e)
        {
            var title = AnalysisTitle.Text;
            var description = AnalysisDescription.Text;

            //TODO: Manage a way to send the newly created data to the database here...
            var analysis = new FullAnalysis<IAnalysisDataRow>(title, description, XHeader, YHeader, this.analysisViewModel.Data);
            

            var resultsPage = new ComputedAnalysis(this.Services, analysis);

            NavigationService.Navigate(resultsPage);
        }
    }
}
