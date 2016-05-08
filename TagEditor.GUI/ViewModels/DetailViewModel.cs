using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Windows.Storage;
using TagEditor.Core.Common;
using TagEditor.GUI.Commands;
using TagEditor.GUI.Models;

namespace TagEditor.GUI.ViewModels
{
    public class DetailViewModel : NotificationBase
    {
        private bool isBusy;
        private string currentFileName;
        private TagViewModel tag;
        private bool moreFiles;
        private FileInformation fileInformation;

        public DetailViewModel()
        {
            PlayCommand = new PlayCommand(this);
            SaveCommand = new SaveCommand(this);
            RemoveImageCommand = new RelayCommand(() => Tag.AlbumArt = null);
            LoadImageCommand = new LoadImageCommand(this);
        }

        public async Task LoadItem(string[] paths)
        {
            IsBusy = true;

            Paths = paths;
            moreFiles = paths.Length > 1;
            if (!moreFiles)
            {
                var currentFile = await StorageFile.GetFileFromPathAsync(paths.First());

                CurrentFileName = currentFile.DisplayName;

                FileInformation = await FileInformation.Load(currentFile);

                if (currentFile.FileType == ".mp3")
                {
                    Tag = await TagViewModel.LoadFromFile(currentFile);
                }
                else
                {
                    Tag = await TagViewModel.LoadOthers(currentFile);
                }
            }

            IsBusy = false;
        }

        public ICommand RemoveImageCommand { get; private set; }
        public ICommand LoadImageCommand { get; private set; }
        public ICommand PlayCommand { get; private set; }
        public ICommand SaveCommand { get; private set; }

        public string[] Paths { get; set; }

        public bool MoreFiles
        {
            get { return moreFiles; }
            set { SetProperty(ref moreFiles, value); }
        }

        public TagViewModel Tag
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

        public FileInformation FileInformation
        {
            get { return fileInformation; }
            set { SetProperty(ref fileInformation, value); }
        }
    }
}