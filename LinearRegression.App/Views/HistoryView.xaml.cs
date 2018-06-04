using LinearRegression.App.Contracts;
using LinearRegression.App.Contracts.Services;
using LinearRegression.App.CustomControls;
using LinearRegression.App.Models;
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

            MainGrid.Children.Add(new AnalysisListView(services, OnItemSelect));
        }

        public IServiceProvider Services { get; }

        public string PageTitle => "History";

        public IHelpContent HelpContent => new HelpContent(
                "Browse history",
                "On this page you can view every analysis you have ever made and see its results.\n" +
                "You can also use the searchbar to filter the results by title"
            );


        private void OnItemSelect(object sender, MouseButtonEventArgs e)
        {
            var selectedItem = ((ListView)sender).SelectedItem as IAnalysisMetadata;

            if(selectedItem!=null)
            {
                var fullAnalysis = dbService.GetFullAnalysis(selectedItem.DatabaseId);
                var computedAnalysisPage = new ComputedAnalysis(Services, fullAnalysis);

                NavigationService.Navigate(computedAnalysisPage);
            }
        }
    }
}
