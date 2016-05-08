using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Windows.Storage;
using Windows.UI.Xaml.Media.Imaging;
using TagEditor.Core.Common;
using TagEditor.GUI.Utility;
using TagEditor.GUI.ViewModels;
using TagLib;
using PictureType = TagEditor.Core.ID3v2.Frame.Types.PictureType;

namespace TagEditor.GUI.Models
{
    public class TagViewModel : NotificationBase
    {
        private string artist;
        private string title;
        private string album;
        private uint? year;
        private uint trackNumber;
        private BitmapImage albumArt;
        private string genre;
        private byte[] albumArtContent;
        private string mimeType;
        private uint trackCount;

        public static async Task<TagViewModel> LoadFromFile(StorageFile file)
        {
            using (var audioFile = new AudioFile(await file.OpenStreamForReadAsync(), true))
            {
                var editor = new Core.Common.TagEditor();

                var info = await editor.RetrieveTagsAsync(audioFile, TagType.ID3v2);

                if (info != null)
                {
                    var tag = new TagViewModel()
                    {
                        Artist = info.Artist.Content,
                        Title = info.Title.Content,
                        Album = info.Album.Content,
                        Year = (uint?)info.Year.Content,
                        TrackNumber = info.TrackNumber.Content ?? 0,
                        TrackCount = info.TrackNumber.TrackCount ?? 0,
                        Genre = info.Genre.Content.ToString()
                    };

                    if (info.AlbumArt.Content != null)
                    {
                        await tag.SetNewImage(info.AlbumArt.Content, info.AlbumArt.MimeType);
                    }

                    return tag;
                }
            }

            return new TagViewModel();
        }

        public static async Task<TagViewModel> LoadOthers(StorageFile file)
        {
            using (var ms = await file.OpenStreamForReadAsync())
            {

                var tagFile = TagLib.File.Create(new StreamFileAbstraction(file.Name, ms, ms));

                Tag tags = null;
                if (file.FileType == ".flac")
                {
                    tags = tagFile.GetTag(TagTypes.FlacMetadata);
                }
                else if (file.FileType == ".m4a")
                {
                    tags = tagFile.GetTag(TagTypes.Apple);
                }

                if (tags != null)
                {
                    var result = new TagViewModel();
                    result.Album = tags.Album;
                    result.Artist = tags.AlbumArtists.FirstOrDefault();
                    result.Title = tags.Title;
                    result.Year = tags.Year;
                    result.TrackNumber = tags.Track;
                    result.TrackCount = tags.TrackCount;
                    result.Genre = tags.FirstGenre;

                    var art = tags.Pictures.FirstOrDefault(pic => pic.Type == TagLib.PictureType.FrontCover);
                    if (art != null)
                    {
                        await result.SetNewImage(art.Data.Data, art.MimeType);
                    }

                    return result;
                }
            }

            return new TagViewModel();
        }

        public void ToTag(Tag tag)
        {
            tag.Title = Title;
            //tag.AlbumArtists = this.AlbumArtist.Split(';').Select(x => x.Trim()).ToArray();
            tag.Performers = new [] { Artist };
            tag.Album = Album;
            tag.Year = Year ?? 0;
            tag.Track = TrackNumber;
            tag.TrackCount = TrackCount;

            if (AlbumArt != null)
            {
                IPicture newArt = new Picture(
                    ByteVector.FromStream(
                        new MemoryStream(albumArtContent)));
                newArt.Type = (TagLib.PictureType)PictureType.FrontCover;
                newArt.MimeType = mimeType;
                // IPicture newArt = this.Picture;
                tag.Pictures = new [] { newArt };
            }
        }

        public TagInformation ToTagInformation()
        {
            var info = new TagInformation();

            if (!string.IsNullOrEmpty(Album))
            {
                info.Album.SetValue(Album);
            }

            if (!string.IsNullOrEmpty(Artist))
            {
                info.Artist.SetValue(Artist);
            }

            if (!string.IsNullOrEmpty(Title))
            {
                info.Title.SetValue(Title);
            }

            if (Year != null)
            {
                info.Year.SetValue((int?)Year);
            }

            if (TrackNumber != null)
            {
                info.TrackNumber.SetValue(TrackNumber);
                info.TrackNumber.TrackCount = TrackCount;
            }

            if (AlbumArt != null)
            {
                info.AlbumArt.Type = PictureType.FrontCover;
                info.AlbumArt.MimeType = mimeType;
                info.AlbumArt.SetValue(albumArtContent);
            }

            return info;
        }


        public async Task SetNewImage(byte[] content, string mimeType)
        {
            albumArtContent = content;
            this.mimeType = mimeType;
            AlbumArt = await content.ToImage();
        }

        public string Artist
        {
            get { return artist; }
            set { SetProperty(ref artist, value); }
        }

        public string Title
        {
            get { return title; }
            set { SetProperty(ref title, value); }
        }

        public string Album
        {
            get { return album; }
            set { SetProperty(ref album, value); }
        }

        public uint? Year
        {
            get { return year; }
            set { SetProperty(ref year, value); }
        }

        public uint TrackNumber
        {
            get { return trackNumber; }
            set { SetProperty(ref trackNumber, value); }
        }

        public uint TrackCount
        {
            get { return trackCount; }
            set { SetProperty(ref trackCount, value); }
        }

        public BitmapImage AlbumArt
        {
            get { return albumArt; }
            set { SetProperty(ref albumArt, value); }
        }

        public string Genre
        {
            get { return genre; }
            set { SetProperty(ref genre, value); }
        }
    }
}
