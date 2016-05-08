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
        private readonly MainViewModel viewModel;
        public MasterPage()
        {
            this.InitializeComponent();
            DataContext = viewModel = new MainViewModel();
        }

        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
            if (Frame.CanGoBack)
            {
                SystemNavigationManager.GetForCurrentView().AppViewBackButtonVisibility = AppViewBackButtonVisibility.Visible;
                SystemNavigationManager.GetForCurrentView().BackRequested += MasterPage_BackRequested;
            }

            var path = (string)e.Parameter;

            if (string.IsNullOrEmpty(path))
            {
                var library = await StorageLibrary.GetLibraryAsync(KnownLibraryId.Music);
                path = library.SaveFolder.Path;
            }

            await viewModel.LoadItems(path);

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
            var selectedFolder = GridView.SelectedItem as FolderItem;
            var selectedFile = GridView.SelectedItem as FileItem;
            if (selectedFolder != null)
            {
                Frame.Navigate(typeof(MasterPage), selectedFolder.Folder.Path);
            }
            else if (selectedFile != null)
            {
                Frame.Navigate(typeof(DetailPage), selectedFile.File.Path);
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
