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

namespace LinearRegression.App.CustomControls
{
    /// <summary>
    /// Interaction logic for AnalysisListView.xaml
    /// </summary>
    public partial class AnalysisListView : UserControl
    {
        private IServiceProvider services;
        private IDatabaseService dbService;
        private ObservableCollection<IAnalysisMetadata> data;
        private Action<object, MouseButtonEventArgs> onListViewItemSelectHandler;
        private Button searchButton;
        private TextBox searchBox;

        public AnalysisListView(IServiceProvider services, Action<object, MouseButtonEventArgs> onListViewItemSelectHandler)
        {
            InitializeComponent();
            this.services = services;
            this.onListViewItemSelectHandler = onListViewItemSelectHandler;
            this.dbService = services.GetService(typeof(IDatabaseService)) as IDatabaseService;

            LoadDataFromDatabase();

            var mainWindow = services.GetService(typeof(MainWindow)) as MainWindow;
            this.searchButton = mainWindow.FindBtn;
            this.searchBox = mainWindow.SearchBox;

            searchButton.Click += OnSearchBtnClick;
        }

        private void AnalysisMetadataList_PreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e) =>
            onListViewItemSelectHandler(sender, e);

        private void LoadDataFromDatabase()
        {
            data = new ObservableCollection<IAnalysisMetadata>(this.dbService.GetAllEntities());
            AnalysisMetadataList.ItemsSource = data;
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
                data = new ObservableCollection<IAnalysisMetadata>(data.Where(d => d.Title.ToLower().Contains(keyword)));
                AnalysisMetadataList.ItemsSource = data;
            }
        }
    }
}
