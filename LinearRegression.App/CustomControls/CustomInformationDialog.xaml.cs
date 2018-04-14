using LinearRegression.App.Contracts;
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

namespace LinearRegression.App.CustomControls
{
    /// <summary>
    /// Interaction logic for CustomInformationDialog.xaml
    /// </summary>
    public partial class CustomInformationDialog : UserControl
    {
        public CustomInformationDialog(IHelpContent helpContent)
        {
            InitializeComponent();

            HelpDialogTitle.Text = helpContent.Title;
            HelpContent.Text = helpContent.Content;
            
        }

    }
}
