using LinearRegression.App.Contracts;
using LinearRegression.App.CustomControls;
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
        private Page currentPage;

        public MainWindow()
        {
            InitializeComponent();
            PageFrame.Content = new HomeView();
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
    }
}
