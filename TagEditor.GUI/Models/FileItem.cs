using System;
using System.Threading.Tasks;
using Windows.Storage;
using Windows.Storage.FileProperties;
using Windows.UI.Xaml.Media.Imaging;

namespace TagEditor.GUI.Models
{
    public class FileItem : GridItem
    {
        public FileItem(StorageFile file)
        {
            File = file;
            Title = File.DisplayName;
        }

        public StorageFile File { get; }

        public override async Task LoadContent()
        {
            if (File.FileType == "mp3")
            {

            }
        }

    }
}