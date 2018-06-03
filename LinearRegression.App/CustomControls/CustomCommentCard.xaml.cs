using LinearRegression.Database.ModelContracts;
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
    /// Interaction logic for CustomCommentCard.xaml
    /// </summary>
    public partial class CustomCommentCard : UserControl
    {
        private IComment comment;

        public CustomCommentCard(IComment comment)
        {
            InitializeComponent();
            this.comment = comment;

            InitializeComment();
        }

        private void InitializeComment()
        {
            UsernameTextBlock.Text += comment.Username;
            ContentTextBlock.Text = comment.Content;
        }
    }
}
