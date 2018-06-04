using LinearRegression.App.Contracts;
using LinearRegression.App.Contracts.Services;
using LinearRegression.App.CustomControls;
using LinearRegression.App.Models;
using LinearRegression.App.ServiceAdapters;
using LinearRegression.App.Views;
using LinearRegression.Database;
using LinearRegression.Database.ModelAdapters;
using LinearRegression.Database.ModelContracts;
using MaterialDesignThemes.Wpf;
using Microsoft.Extensions.DependencyInjection;
using Moq;
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

namespace LinearRegression.App
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Page currentPage;
        private IServiceProvider services;

        public MainWindow()
        {
            InitializeComponent();
            this.services = ConfigureServices(this);

            PageFrame.Content = new HomeView(this.services);
        }

        private void HelpButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var helpContent = ((ICustomPage)this.currentPage).HelpContent;

                var helpDialog = new CustomInformationDialog(helpContent);
                DialogHost.Show(helpDialog);
            }
            catch (Exception) { }
        }

        private void PageFrame_ContentRendered(object sender, EventArgs e)
        {
            var page = PageFrame.Content;
            this.currentPage = page as Page;

            HomeButton.Visibility = page is HomeView ? Visibility.Hidden : Visibility.Visible;

            SearchUI.Visibility = page is HistoryView ? Visibility.Visible : Visibility.Hidden;

            PageTitleHolder.Text = ((ICustomPage)page).PageTitle;
        }

        private static IServiceProvider ConfigureServices(MainWindow mainWindowInstance)
        {
            var services = new ServiceCollection();

            services.AddSingleton<IModelController<LinearRegressionDbContext>>(new ModelController<LinearRegressionDbContext>("LinearRegression.Database.Model"));
            services.AddSingleton<IDatabaseService>(sp=>new DatabaseService(sp));
            services.AddSingleton<IAnalysisLogicService>(sp=>new AnalysisLogicService(sp));
            services.AddSingleton(mainWindowInstance);

            var serviceProvider = services.BuildServiceProvider();
            return serviceProvider;
        }

        private void HomeButton_Click(object sender, RoutedEventArgs e)
        {
            PageFrame.Content = new HomeView(this.services);
        }
    }
}
