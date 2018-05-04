using LinearRegression.App.Contracts;
using LinearRegression.App.Contracts.Services;
using LinearRegression.App.CustomControls;
using LinearRegression.App.Models;
using LinearRegression.App.Views;
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
            this.services = ConfigureServices();

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

            PageTitleHolder.Text = ((ICustomPage)page).PageTitle;
        }

        private static IServiceProvider ConfigureServices()
        {
            var services = new ServiceCollection();

            var analysisServiceMock = new Mock<IAnalysisLogicService>();

            //Only for testing
            analysisServiceMock.Setup(asm => asm.GetAdjustedData(It.IsAny<IAnalysisData<IAnalysisDataRow>>())).Returns<IAnalysisData<IAnalysisDataRow>>(adr=> 
            {
                
                var collection = new ObservableCollection<IAdjustedDataRow>();

                foreach (var row in adr.Data)
                    collection.Add(new AdjustedDataRow(row.Index, row.X, row.Y, row.Y));

                var ad = new FullAnalysis<IAdjustedDataRow>("bla", "bla", "X", "Y", collection);

                return ad;
            });

            //TODO: Replace with working service!
            services.AddSingleton(analysisServiceMock.Object);

            var serviceProvider = services.BuildServiceProvider();
            return serviceProvider;
        }
    }
}
