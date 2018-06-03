using LinearRegression.App.Contracts;
using LinearRegression.App.Contracts.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    /// Interaction logic for HistoryView.xaml
    /// </summary>
    public partial class HistoryView : Page, ICustomPage
    {
        private IDatabaseService dbService;

        public HistoryView(IServiceProvider services)
        {
            InitializeComponent();

            this.Services = services;

            this.dbService = this.Services.GetService(typeof(IDatabaseService)) as IDatabaseService;

            LoadDataFromDatabase();
        }

        public IServiceProvider Services { get; }

        public string PageTitle => "History";

        public IHelpContent HelpContent => throw new NotImplementedException();

        private void LoadDataFromDatabase()
        {
            var oc = new ObservableCollection<IAnalysisMetadata>(this.dbService.GetAllEntities());
            AnalysisMetadataList.ItemsSource = oc;
        }

        private void AnalysisMetadataList_PreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            var selectedItem = ((ListView)sender).SelectedItem as IAnalysisMetadata;
            var fullAnalysis = dbService.GetFullAnalysis(selectedItem.DatabaseId);
            var computedAnalysisPage = new ComputedAnalysis(Services, fullAnalysis);

            NavigationService.Navigate(computedAnalysisPage);
        }
    }
}
