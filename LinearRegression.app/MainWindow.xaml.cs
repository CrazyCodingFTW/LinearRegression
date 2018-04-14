using LinearRegression.App.CustomControls;
using LinearRegression.App.StaticData;
using LinearRegression.App.Views;
using MaterialDesignThemes.Wpf;
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

namespace LinearRegression.App
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            PageFrame.Content = new HomeView(PageTitleHolder);

            CurrentPage = typeof(HomeView);
        }

        public static Type CurrentPage { get; set; }

        private void HelpButton_Click(object sender, RoutedEventArgs e)
        {
            var helpContent = HelpContentTexts.GetHelpContent(CurrentPage);
            
            var helpDialog = new CustomInformationDialog(helpContent);
            DialogHost.Show(helpDialog);
        }
    }
}
