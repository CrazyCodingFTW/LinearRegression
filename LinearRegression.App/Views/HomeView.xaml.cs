using LinearRegression.App.Contracts;
using LinearRegression.App.Models;
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
    public partial class HomeView : Page, ICustomPage
    {
        private const ShadowDepth DarkShadowDepth = ShadowDepth.Depth4;

        private ShadowDepth defaultShadowDepth;

        public HomeView(IServiceProvider services)
        {
            InitializeComponent();

            this.Services = services;
        }

        public string PageTitle => "Home";

        public IHelpContent HelpContent => 
            new HelpContent(this.PageTitle,
                $"In the home view you have three options to choose from:{Environment.NewLine}{Environment.NewLine}New Analysis - creates new Linear Regression analysis{Environment.NewLine}{Environment.NewLine}History - browse previous analysis{Environment.NewLine}{Environment.NewLine}Export Data - Choose analysis to export into Excel");

        public IServiceProvider Services { get; }

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

        private void NewAnalysisCard_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            var newAnalysis = new NewAnalysis(this.Services);
            NavigationService.Navigate(newAnalysis);
        }
    }
}
