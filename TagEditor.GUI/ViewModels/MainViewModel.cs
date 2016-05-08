using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Windows.Storage;
using TagEditor.Core.Common;
using TagEditor.GUI.Commands;
using TagEditor.GUI.Models;
using TagEditor.GUI.Utility;

namespace TagEditor.GUI.ViewModels
{
    public class MainViewModel : NotificationBase
    {
        private string currentFolderName;
        private bool isBusy;

        public MainViewModel()
        {
            FileItems = new ObservableRangeCollection<GridItem>();
            TestCommand = new RelayCommand(Test);
        }

        public async void Test()
        {
            IsBusy = true;
            await Task.Run(async () =>
            {
                var file = await StorageFile.GetFileFromPathAsync(@"D:\Music\001-adele-hello.mp3");

                using (var fs = await file.OpenStreamForWriteAsync())
                {
                    using (var audioFile = new AudioFile(fs))
                    {
                        var editor = new Core.Common.TagEditor();

                        var info = new TagInformation();
                        info.TrackNumber.SetValue(2);
                        info.Title.SetValue("WTF");
                        info.Artist.SetValue("Artist");


                        await editor.SetTags(audioFile, info, TagType.ID3v2);
                    }
                }
            });
            IsBusy = false;

        }

        public async Task LoadItems(string path)
        {
            IsBusy = true;

            var currentFolger = await StorageFolder.GetFolderFromPathAsync(path);

            CurrentFolderName = currentFolger.DisplayName;

            var files = FilterFiles(await currentFolger.GetFilesAsync());
            var folders = await currentFolger.GetFoldersAsync();

            FileItems.AddRange(folders.Select(folder => new FolderItem(folder)));
            FileItems.AddRange(files.Select(file => new FileItem(file)));

            IsBusy = false;

            foreach (var item in FileItems)
            {
                await item.LoadContent();
            }
        }

        public ICommand TestCommand { get; private set; }

        public bool IsBusy
        {
            get { return isBusy; }
            set { SetProperty(ref isBusy, value); }
        }

        public string CurrentFolderName
        {
            get { return currentFolderName; }
            set { SetProperty(ref currentFolderName, value); }
        }

        public ObservableRangeCollection<GridItem> FileItems { get; private set; }

        private ICollection<StorageFile> FilterFiles(IReadOnlyCollection<StorageFile> files)
        {
            var supportedTypes = new string[] {".mp3", ".flac", ".m4a"};
            return files.Where(file => supportedTypes.Contains(file.FileType)).ToList();
        }
    }
}
