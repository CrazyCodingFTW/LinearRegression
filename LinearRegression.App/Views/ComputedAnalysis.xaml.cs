using LinearRegression.App.Contracts;
using LinearRegression.App.Contracts.Services;
using LinearRegression.App.Models;
using LinearRegression.App.Views.ComputedAnalysisPages;
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

namespace LinearRegression.App.Views
{
    public partial class ComputedAnalysis : Page, ICustomPage
    {
        private IFullAnalysis<IAnalysisDataRow> analysisModel;
        private IAnalysisLogicService analysisLogicService;

        public ComputedAnalysis(IServiceProvider services, IFullAnalysis<IAnalysisDataRow> analysisModel)
        {
            InitializeComponent();
            this.Services = services;

            this.analysisLogicService = Services.GetService(typeof(IAnalysisLogicService)) as IAnalysisLogicService;

            this.analysisModel = analysisModel;

            //TODO: Find if the data had allready been computed and provide the computational data instead of computing it again. It is a good idea to make such constructors.
        }

        public string PageTitle => analysisModel.Title;

        public IHelpContent HelpContent => new HelpContent(
                "Computed analyisis page",
                "This page is made to show you the results of your analysis\n" +
                "Click on any of the expanders to see what the results are."
            );

        public IServiceProvider Services { get; }

        private void AnalysisInformationExpander_Expanded(object sender, RoutedEventArgs e)
        {
            TitleField.Text = this.analysisModel.Title;
            DescriptionField.Text = this.analysisModel.Description;
            CreationDateField.Text = this.analysisModel.CreationDate.ToString();
        }

        private void CommonAnalysisData_Expanded(object sender, RoutedEventArgs e)
        {
            var commonData = new CommonAnalysisDataPage(analysisLogicService, analysisModel);

            CADPage.Content = commonData;
        }

        private void AdequacyData_Expanded(object sender, RoutedEventArgs e)
        {
            var adjustedData = analysisLogicService.GetFullAnalysisAdjustedData(analysisModel);
            var calculations = analysisLogicService.GetAnalysisCalculations(analysisModel);
            var adData = new DataAdequacyPage(adjustedData, calculations);

            ModelAdequacyDataPage.Content = adData;
        }

        private void AMError_Expanded(object sender, RoutedEventArgs e)
        {
            var calculations = analysisLogicService.GetAnalysisCalculations(analysisModel);
            var amErrorsPage = new AverageAndMaximalErrorsPage(calculations);

            AMErrorPage.Content = amErrorsPage;
        }

        private void Comments_Expanded(object sender, RoutedEventArgs e)
        {
            var fullAnalysis = (FullAnalysis<IAnalysisDataRow>)analysisModel;
            var dbService = Services.GetService(typeof(IDatabaseService)) as IDatabaseService;
            var commentsPage = new CommentsViewPage(analysisModel.DatabaseId, dbService);
            CommentsPage.Content = commentsPage;
        }
    }
}
