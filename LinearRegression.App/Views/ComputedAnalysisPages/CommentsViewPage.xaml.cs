using LinearRegression.App.Contracts.Services;
using LinearRegression.App.CustomControls;
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

namespace LinearRegression.App.Views.ComputedAnalysisPages
{
    /// <summary>
    /// Interaction logic for CommentsViewPage.xaml
    /// </summary>
    public partial class CommentsViewPage : Page
    {
        private long analysisInformationId;
        private IDatabaseService databaseService;

        public CommentsViewPage(long analysisInformationId, IDatabaseService databaseService)
        {
            InitializeComponent();
            this.analysisInformationId = analysisInformationId;
            this.databaseService = databaseService;

            InitializeComments();
        }

        private async void InitializeComments()
        {
            var comments = databaseService.GetAnalysisComments(analysisInformationId);
            foreach(var comment in comments)
            {
                var commentCard = new CustomCommentCard(comment);
                CommentsField.Children.Add(commentCard);
            }
        }

        private void SubmitCommentBtn_Click(object sender, RoutedEventArgs e)
        {
            var newComment = databaseService.AddComment(analysisInformationId, UsernameTextBlock.Text, CommentContentTextBlock.Text);
            CommentsField.Children.Add(new CustomCommentCard(newComment));
            UsernameTextBlock.Text = string.Empty;
            CommentContentTextBlock.Text = string.Empty;
        }
    }
}
