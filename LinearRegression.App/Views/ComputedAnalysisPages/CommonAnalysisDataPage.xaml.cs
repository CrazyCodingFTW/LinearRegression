using LinearRegression.App.Contracts;
using LinearRegression.App.Contracts.Services;
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
    public partial class CommonAnalysisDataPage : Page
    {
        private IAnalysisData<IAdjustedDataRow> analysisData;
        private IAnalysisLogicService analysisLogicService;

        public CommonAnalysisDataPage(IAnalysisLogicService analysisLogicService, IAnalysisData<IAnalysisDataRow> modelAnalysisData)
        {
            this.analysisLogicService = analysisLogicService;

            //TODO: Implement!
            this.analysisData = analysisLogicService.GetAdjustedData(modelAnalysisData);

            InitializeComponent();

            InitializeContent();
        }

        private void InitializeContent()
        {
            DataGrid.ItemsSource = this.analysisData.Data;
            DataChart.ItemsSource = this.analysisData.Data;

            var xHeader = this.analysisData.XMeaning == "X" ? this.analysisData.XMeaning : this.analysisData.XMeaning + " (X)";
            var yHeader = this.analysisData.YMeaning == "Y" ? this.analysisData.YMeaning : this.analysisData.YMeaning + " (Y)";

            XColumn.HeaderText = xHeader;
            XAxis.Header = xHeader;

            YColumn.HeaderText = yHeader;
            YAxis.Header = yHeader;
        }
    }
}
