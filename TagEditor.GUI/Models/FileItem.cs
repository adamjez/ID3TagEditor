using System;
using System.IO;
using System.Threading.Tasks;
using Windows.Storage;
using Windows.Storage.FileProperties;
using Windows.UI.Xaml.Media.Imaging;
using TagEditor.Core.Common;
using TagLib;

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

            var stream = await File.OpenStreamForReadAsync();
            if (File.FileType == ".mp3")
            {
                await LoadMp3(stream);
            }
            else
            {
                LoadOthers(stream);
            }
        }

        private void LoadOthers(Stream stream)
        {
            var tagFile = TagLib.File.Create(new StreamFileAbstraction(File.Name,
                stream, stream));

            TagLib.Tag tags = null;
            if (File.FileType == ".flac")
            {
                tags = tagFile.GetTag(TagTypes.FlacMetadata);
            }
            else if (File.FileType == ".m4a")
            {
                tags = tagFile.GetTag(TagTypes.Apple);
            }

            if (tags != null)
            {
                Description1 = tags.Title;
                Description2 = tags.FirstAlbumArtist;
            }
        }

        private async Task LoadMp3(Stream stream)
        {
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