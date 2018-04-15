using LinearRegression.App.Contracts;
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
    /// <summary>
    /// Interaction logic for NewAnalysis.xaml
    /// </summary>
    public partial class NewAnalysis : Page, ICustomPage
    {
        public NewAnalysis()
        {
            InitializeComponent();
        }

        public string PageTitle => "New Analysis";

        public IHelpContent HelpContent => throw new NotImplementedException();

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            var homeView = new HomeView();
            NavigationService.Navigate(homeView);
        }
    }
}
