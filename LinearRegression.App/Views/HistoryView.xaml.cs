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
        private Button searchButton;
        private TextBox searchBox;
        private ObservableCollection<IAnalysisMetadata> data;

        public HistoryView(IServiceProvider services)
        {
            InitializeComponent();

            this.Services = services;

            this.dbService = this.Services.GetService(typeof(IDatabaseService)) as IDatabaseService;

            LoadDataFromDatabase();

            var mainWindow = services.GetService(typeof(MainWindow)) as MainWindow;
            this.searchButton = mainWindow.FindBtn;
            this.searchBox = mainWindow.SearchBox;

            searchButton.Click += OnSearchBtnClick;
        }

        public IServiceProvider Services { get; }

        public string PageTitle => "History";

        public IHelpContent HelpContent => throw new NotImplementedException();

        private void LoadDataFromDatabase()
        {
            data = new ObservableCollection<IAnalysisMetadata>(this.dbService.GetAllEntities());
            AnalysisMetadataList.ItemsSource = data;
        }

        private void AnalysisMetadataList_PreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            var selectedItem = ((ListView)sender).SelectedItem as IAnalysisMetadata;

            if(selectedItem!=null)
            {
                var fullAnalysis = dbService.GetFullAnalysis(selectedItem.DatabaseId);
                var computedAnalysisPage = new ComputedAnalysis(Services, fullAnalysis);

                NavigationService.Navigate(computedAnalysisPage);
            }
        }

        private void OnSearchBtnClick(object sender, EventArgs e)
        {
            PerformSearch();
        }

        private async void PerformSearch()
        {
            var keyword = searchBox.Text.ToLower();
            if (string.IsNullOrEmpty(keyword))
                LoadDataFromDatabase();
            else
            {
                var foundData = new ObservableCollection<IAnalysisMetadata>();
                foreach (var item in data)
                {
                    if (item.Title.ToLower().Contains(keyword))
                        foundData.Add(item);
                }

                data = foundData;

                AnalysisMetadataList.ItemsSource = data;
            }
        }
    }
}
