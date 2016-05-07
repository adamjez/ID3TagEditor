using System;
using System.IO;
using System.Threading.Tasks;
using Windows.Storage;
using Windows.Storage.FileProperties;
using Windows.UI.Xaml.Media.Imaging;
using TagEditor.Core.Common;
using TagEditor.GUI.Common;

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
            var thumbnail = await File.GetThumbnailAsync(ThumbnailMode.MusicView);
            Thumbnail = new BitmapImage();
            Thumbnail.SetSource(thumbnail);

            if (File.FileType == ".mp3")
            {
                var stream = (await File.OpenAsync(FileAccessMode.Read)).AsStream();
                using (var file = new AudioFile(stream, true))
                {
                    var editor = new Core.Common.TagEditor();

                    var info = await editor.RetrieveBasicTagsAsync(file);

                    if (info != null)
                    {
                        Description1 = info.Title.Content;
                        Description2 = info.Artist.Content;
                    }
                }
            }
        }

    }
}