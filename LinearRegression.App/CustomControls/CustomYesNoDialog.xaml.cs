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
    /// Interaction logic for CustomYesNoDialog.xaml
    /// </summary>
    public partial class CustomYesNoDialog : UserControl
    {
        public CustomYesNoDialog(string questionText)
        {
            InitializeComponent();
            QuestionText.Text = questionText;
        }

        public bool ClosedWithConfirmation { get; private set; }

        private void ConfirmButton_Click(object sender, RoutedEventArgs e) => ClosedWithConfirmation = true;

        private void DismissButton_Click(object sender, RoutedEventArgs e) => ClosedWithConfirmation = false;
    }
}
