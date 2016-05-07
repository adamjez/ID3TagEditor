using System;
using System.Threading.Tasks;
using Windows.Storage;
using Windows.Storage.FileProperties;
using Windows.UI.Xaml.Media.Imaging;

namespace TagEditor.GUI.Models
{
    public class FolderItem : GridItem
    {
        public FolderItem(StorageFolder folder)
        {
            Folder = folder;
            Title = folder.DisplayName;
            
        }

        public StorageFolder Folder { get;}

        public override async Task LoadContent()
        {
            var thumbnail = await Folder.GetThumbnailAsync(ThumbnailMode.PicturesView);
            //Thumbnail = new BitmapImage();
            //Thumbnail.SetSource(thumbnail);
        }
    }
}