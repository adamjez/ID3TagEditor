using System;
using System.Linq;
using Windows.Storage;
using Windows.UI.Core;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;
using TagEditor.GUI.ViewModels;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace TagEditor.GUI.Pages
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class DetailPage : Page
    {
        private readonly DetailViewModel viewModel;
        public DetailPage()
        {
            this.InitializeComponent();
            DataContext = viewModel = new DetailViewModel();
        }

        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
            SystemNavigationManager.GetForCurrentView().AppViewBackButtonVisibility = AppViewBackButtonVisibility.Visible;
            SystemNavigationManager.GetForCurrentView().BackRequested += DetailPage_BackRequested;

            var pathsString = (string)e.Parameter;

            if (string.IsNullOrEmpty(pathsString))
            {
                throw new ArgumentException("No parameter given to navigation");
            }

             await viewModel.LoadItem(pathsString.Split(';'));


            base.OnNavigatedTo(e);
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            SystemNavigationManager.GetForCurrentView().BackRequested -= DetailPage_BackRequested;

            base.OnNavigatedFrom(e);
        }

        private void DetailPage_BackRequested(object sender, BackRequestedEventArgs e)
        {
            if (Frame.CanGoBack)
            {
                Frame.GoBack();
                e.Handled = true;
            }
        }
    }
}
