using System;
using System.Drawing;
using System.IO;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.Storage.Streams;
using Windows.UI.Xaml.Media.Imaging;
using TagEditor.Core.Common;
using TagEditor.GUI.ViewModels;

namespace TagEditor.GUI.Models
{
    public class Tag : NotificationBase
    {
        private string artist;
        private string title;
        private string album;
        private uint? year;
        private uint? trackNumber;
        private BitmapImage albumArt;



        public static async Task<Tag> LoadFromTag(TagInformation info)
        {
            var tag = new Tag()
            {
                Artist = info.Artist.Content,
                Title = info.Title.Content,
                Album = info.Album.Content,
                Year = (uint?)info.Year.Content,
                TrackNumber = info.TrackNumber.Content
            };

            if (info.AlbumArt.Content != null)
            {
                var bitmap = new BitmapImage();
                using (var ms = new MemoryStream(info.AlbumArt.Content))
                {
                    await bitmap.SetSourceAsync(ms.AsRandomAccessStream());
                }
            }

            return tag;
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

        public uint? TrackNumber
        {
            get { return trackNumber; }
            set { SetProperty(ref trackNumber, value); }
        }

        public BitmapImage AlbumArt
        {
            get { return albumArt; }
            set { SetProperty(ref albumArt, value); }
        }
    }
}
