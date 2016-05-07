using System;
using System.Threading.Tasks;
using Windows.Storage;
using TagEditor.GUI.Models;

namespace TagEditor.GUI.ViewModels
{
    public class DetailViewModel : NotificationBase
    {
        private bool isBusy;
        private string currentFileName;
        private Tag tag;

        public DetailViewModel()
        {
        }

        public async Task LoadItem(string path)
        {
            IsBusy = true;

            var currentFile = await StorageFile.GetFileFromPathAsync(path);

            CurrentFileName = currentFile.DisplayName;


            IsBusy = false;
        }

        public Tag Tag
        {
            get { return tag; }
            set { SetProperty(ref tag, value); }
        }

        public bool IsBusy
        {
            get { return isBusy; }
            set { SetProperty(ref isBusy, value); }
        }

        public string CurrentFileName
        {
            get { return currentFileName; }
            set { SetProperty(ref currentFileName, value); }
        }
    }
}