using System;
using Windows.Storage;
using Windows.UI.Core;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;
using TagEditor.GUI.Models;
using TagEditor.GUI.ViewModels;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace TagEditor.GUI.Pages
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MasterPage : Page
    {
        private MainViewModel ViewModel;
        public MasterPage()
        {
            this.InitializeComponent();
            DataContext = ViewModel = new MainViewModel();
        }

        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
            if (Frame.CanGoBack)
            {
                SystemNavigationManager.GetForCurrentView().AppViewBackButtonVisibility = AppViewBackButtonVisibility.Visible;
                SystemNavigationManager.GetForCurrentView().BackRequested += MasterPage_BackRequested;
            }

            var path = (string) e.Parameter;

            if (string.IsNullOrEmpty(path))
            {
                var library = await StorageLibrary.GetLibraryAsync(KnownLibraryId.Music);
                path = library.SaveFolder.Path;
            }

            await ViewModel.LoadItems(path);

            base.OnNavigatedTo(e);
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            if (Frame.CanGoBack)
            {
                SystemNavigationManager.GetForCurrentView().BackRequested -= MasterPage_BackRequested;
            }
            base.OnNavigatedFrom(e);
        }

        private void Selector_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var selectedItem = (FolderItem)GridView.SelectedItem;
            if (selectedItem != null)
            {
                Frame.Navigate(typeof(MasterPage), selectedItem.Folder.Path);
            }
        }

        private void MasterPage_BackRequested(object sender, BackRequestedEventArgs e)
        {
            if (Frame.CanGoBack)
            {
                Frame.GoBack();
                e.Handled = true;
            }
        }
    }
}
