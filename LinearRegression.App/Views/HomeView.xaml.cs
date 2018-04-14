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

namespace LinearRegression.App.Views
{
    /// <summary>
    /// Interaction logic for HomeView.xaml
    /// </summary>
    public partial class HomeView : Page
    {
        private const ShadowDepth DarkShadowDepth = ShadowDepth.Depth4;

        private ShadowDepth defaultShadowDepth;

        public HomeView(TextBlock pageTitleHolder)
        {
            InitializeComponent();

            var thisName = this.GetType().Name;
            pageTitleHolder.Text = thisName.Substring(0, thisName.LastIndexOf("View"));
        }

        /// <summary>
        /// Make the shadow of a card darken when hoover
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Card_MouseEnter(object sender, MouseEventArgs e)
        {
            var card = sender as Card;

            this.defaultShadowDepth = ShadowAssist.GetShadowDepth(card);

            ShadowAssist.SetShadowDepth(card, DarkShadowDepth);
        }

        /// <summary>
        /// Changes the card back to its default shadow depth
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Card_MouseLeave(object sender, MouseEventArgs e)
        {
            var card = sender as Card;

            ShadowAssist.SetShadowDepth(card, this.defaultShadowDepth);
        }
    }
}
