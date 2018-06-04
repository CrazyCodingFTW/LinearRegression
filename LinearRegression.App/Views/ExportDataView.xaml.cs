using LinearRegression.App.Contracts;
using LinearRegression.App.Contracts.Services;
using LinearRegression.App.CustomControls;
using LinearRegression.App.Models;
using LinearRegression.App.Views.ComputedAnalysisPages;
using MaterialDesignThemes.Wpf;
using Syncfusion.UI.Xaml.Grid;
using Syncfusion.UI.Xaml.Grid.Converter;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
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

namespace LinearRegression.App.Views
{
    /// <summary>
    /// Interaction logic for ExportDataView.xaml
    /// </summary>
    public partial class ExportDataView : Page, ICustomPage
    {
        public ExportDataView(IServiceProvider services)
        {
            InitializeComponent();
            this.Services = services;

            MainGrid.Children.Add(new AnalysisListView(services, OnItemSelect));
        }

        public IServiceProvider Services { get; }

        public string PageTitle => "Export data";

        public IHelpContent HelpContent => new HelpContent(
                "Export data",
                "Here you can export any data you desire to xlsx format."
            );

        private async void OnItemSelect(object sender, MouseButtonEventArgs e)
        {
            var selectedItem = ((ListView)sender).SelectedItem as IAnalysisMetadata;

            if (selectedItem != null)
            {
                var logicService = Services.GetService(typeof(IAnalysisLogicService)) as IAnalysisLogicService;
                var dbService = Services.GetService(typeof(IDatabaseService)) as IDatabaseService;

                var fullAnalysis = dbService.GetFullAnalysis(selectedItem.DatabaseId);

                //Instantiating CommonAnalysisDataPage to obtain the DataGrid of the analysis
                var commonAnalysisDataPage = new CommonAnalysisDataPage(logicService, fullAnalysis);
                DataGridView.Content = commonAnalysisDataPage;

                var yesNoDialog = new CustomYesNoDialog("Do you want to export the selected item?");
                await DialogHost.Show(yesNoDialog);

                if (yesNoDialog.ClosedWithConfirmation)
                {
                    var dataGrid = commonAnalysisDataPage.DataGrid;

                    var exportLocation = Directory.CreateDirectory($@"{Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments)}\LinearRegression\Exports");
                    ExportToExcel(selectedItem.Title, dataGrid, exportLocation);

                    ExportCompletedSnackbar.MessageQueue.Enqueue("Export complete!");
                    Process.Start(exportLocation.FullName);
                }
            }
        }

        /// <summary>
        /// Exports a datagrid to excel
        /// </summary>
        /// <param name="dataGrid"></param>
        private void ExportToExcel(string analysisName, SfDataGrid dataGrid, DirectoryInfo exportLocation)
        {
            var options = new ExcelExportingOptions();
            options.ExcelVersion = Syncfusion.XlsIO.ExcelVersion.Excel2016;

            var view = dataGrid.View;
            using (var excelEngine = dataGrid.ExportToExcel(view, options))
            {
                var workBook = excelEngine.Excel.Workbooks[0];
                workBook.SaveAs($@"{exportLocation.FullName}\{analysisName}.xlsx");
            }
        }
    }
}
