using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Windows.Storage;
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
        private ObservableCollection<FileInformation> fileInformations;

        public DetailViewModel()
        {
            PlayCommand = new PlayCommand(this);
            SaveCommand = new SaveCommand(this);
            RemoveImageCommand = new RemoveImageCommand(this);
            LoadImageCommand = new LoadImageCommand(this);
            fileInformations = new ObservableCollection<FileInformation>();
            RemoveCommand = new RemoveCommand(this);
            MultipleSaveCommand = new MultipleSaveCommand(this);
        }

        public async Task LoadItem(string[] paths)
        {
            IsBusy = true;

            fileInformations.Clear();
            Paths = paths;
            MoreFiles = paths.Length > 1;
            if (!MoreFiles)
            {
                var currentFile = await StorageFile.GetFileFromPathAsync(paths[0]);

                CurrentFileName = currentFile.DisplayName;

                fileInformations.Add(await FileInformation.Load(currentFile));

                if (currentFile.FileType == ".mp3")
                {
                    Tag = await TagCreator.LoadFromFile(currentFile);
                }
                else
                {
                    Tag = await TagCreator.LoadOthers(currentFile);
                }
            }
            else
            {
                CurrentFileName = "Multiple files selected";
                ICollection<StorageFile> files = new List<StorageFile>();
                foreach (var path in paths)
                {
                    var currentFile = await StorageFile.GetFileFromPathAsync(path);

                    files.Add(currentFile);
                    fileInformations.Add(await FileInformation.Load(currentFile));
                }

                if (files.All(file => file.FileType == ".mp3"))
                {
                    Tag = await TagCreator.LoadFromFiles(files);
                }
            }
            IsBusy = false;
        }

        public ICommand RemoveImageCommand { get; private set; }
        public ICommand LoadImageCommand { get; private set; }
        public ICommand PlayCommand { get; private set; }
        public ICommand SaveCommand { get; private set; }
        public ICommand RemoveCommand { get; private set; }
        public ICommand MultipleSaveCommand { get; private set; }

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

        public ObservableCollection<FileInformation> FileInformations
        {
            get { return fileInformations; }
            set { SetProperty(ref fileInformations, value); }
        }
    }
}