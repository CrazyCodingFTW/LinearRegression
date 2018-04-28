using LinearRegression.App.Contracts;
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
        private IFullAnalysis analysisModel;

        public ComputedAnalysis(IFullAnalysis analysisModel)
        {
            InitializeComponent();

            this.analysisModel = analysisModel;
        }

        public string PageTitle => analysisModel.Title;

        public IHelpContent HelpContent => throw new NotImplementedException();

        private void AnalysisInformationExpander_Expanded(object sender, RoutedEventArgs e)
        {
            TitleField.Text = this.analysisModel.Title;
            DescriptionField.Text = this.analysisModel.Description;
            CreationDateField.Text = this.analysisModel.CreationDate.ToString();
        }

        private void CommonAnalysisData_Expanded(object sender, RoutedEventArgs e)
        {
            var commonData = new CommonAnalysisDataPage(analysisModel);

            CADPage.Content = commonData;
        }
    }
}
