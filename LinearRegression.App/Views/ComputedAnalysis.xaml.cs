using LinearRegression.App.Contracts;
using LinearRegression.App.Contracts.Services;
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

        public ComputedAnalysis(IServiceProvider services, IFullAnalysis<IAnalysisDataRow> analysisModel)
        {
            InitializeComponent();

            this.Services = services;
            this.analysisModel = analysisModel;

            //TODO: Find if the data had allready been computed and provide the computational data instead of computing it again. It is a good idea to make such constructors.
        }

        public string PageTitle => analysisModel.Title;

        public IHelpContent HelpContent => throw new NotImplementedException();

        public IServiceProvider Services { get; }

        private void AnalysisInformationExpander_Expanded(object sender, RoutedEventArgs e)
        {
            TitleField.Text = this.analysisModel.Title;
            DescriptionField.Text = this.analysisModel.Description;
            CreationDateField.Text = this.analysisModel.CreationDate.ToString();
        }

        private void CommonAnalysisData_Expanded(object sender, RoutedEventArgs e)
        {
            var analysisLogicService = Services.GetService(typeof(IAnalysisLogicService)) as IAnalysisLogicService;
            var commonData = new CommonAnalysisDataPage(analysisLogicService, analysisModel);

            CADPage.Content = commonData;
        }
    }
}
