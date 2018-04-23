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
    public partial class CustomXYMeaningPrompt : UserControl
    {
        /// <summary>
        /// Creates a prompt box which prompts the user to enter which variable meaning he wants to define
        /// </summary>
        /// <param name="variableOfInterest">Either X or Y</param>
        /// <param name="displayingWindow">The window in which the dialog will be displayed</param>
        public CustomXYMeaningPrompt(Variables variableOfInterest)
        {
            InitializeComponent();

            AssignVariableMeaningTitle.Text = variableOfInterest + AssignVariableMeaningTitle.Text;
            HintAssist.SetHint(MeaningTextField, $"Enter the meaning of {variableOfInterest} here");
        }

        public bool ClosedWithConfirmation { get; private set; }

        public string EnteredText => MeaningTextField.Text;

        public enum Variables { X, Y }

        private void ConfirmButton_OnClick(object sender, RoutedEventArgs e)
        {
            this.ClosedWithConfirmation = true;
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.ClosedWithConfirmation = false;
        }
    }
}
