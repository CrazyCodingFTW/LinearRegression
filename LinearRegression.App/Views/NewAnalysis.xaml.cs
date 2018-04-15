﻿using LinearRegression.App.Contracts;
using LinearRegression.App.Models;
using Syncfusion.UI.Xaml.Grid;
using Syncfusion.UI.Xaml.Grid.Helpers;
using Syncfusion.UI.Xaml.ScrollAxis;
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
using System.Windows.Threading;

namespace LinearRegression.App.Views
{
    /// <summary>
    /// Interaction logic for NewAnalysis.xaml
    /// </summary>
    public partial class NewAnalysis : Page, ICustomPage
    {
        private IViewModel<IAnalysisDataRow> analysisViewModel;

        public NewAnalysis()
        {
            InitializeComponent();

            analysisViewModel = new ViewModel<IAnalysisDataRow>();
            DataGrid.ItemsSource = analysisViewModel.Data;
        }

        public string PageTitle => "New Analysis";

        public IHelpContent HelpContent => throw new NotImplementedException();

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            var homeView = new HomeView();
            NavigationService.Navigate(homeView);

            DataGrid.Dispose();
        }
    }
}
