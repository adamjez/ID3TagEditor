using System;
using System.IO;
using Windows.UI.Xaml.Media.Imaging;
using TagEditor.Core.Common;
using TagEditor.GUI.ViewModels;
using TagLib;
using File = TagLib.File;
using PictureType = TagEditor.Core.ID3v2.Frame.Types.PictureType;

namespace TagEditor.GUI.Models
{
    public class TagViewModel : NotificationBase
    {
        private MultiInfo<string> artist;
        private string title;
        private MultiInfo<string> album;
        private MultiInfo<uint?> year;
        private uint trackNumber;
        private MultiInfo<ImageTag> albumArt;
        private MultiInfo<string> genre;
        private uint trackCount;
        private MultiInfo<string> albumArtist;

        public TagViewModel()
        {
            AlbumArt = new MultiInfo<ImageTag>(true);
            AlbumArtist = new MultiInfo<string>(true);
            Artist = new MultiInfo<string>(true);
            Genre = new MultiInfo<string>(true);
            Album = new MultiInfo<string>(true);
            Year = new MultiInfo<uint?>(true);
        }

        public void ToTag(Tag tag)
        {
            tag.Title = Title;
            tag.Performers = new [] { Artist.Content };
            tag.Album = Album.Content;
            tag.Year = Year.Content ?? 0;
            tag.Track = TrackNumber;
            tag.TrackCount = TrackCount;
            tag.AlbumArtists = new[] {AlbumArtist.Content};

            if (AlbumArt != null)
            {
                IPicture newArt = new Picture(
                    ByteVector.FromStream(
                        new MemoryStream(albumArt.Content.Content)));
                newArt.Type = (TagLib.PictureType)PictureType.FrontCover;
                newArt.MimeType = albumArt.Content.MimeType;
                // IPicture newArt = this.Picture;
                tag.Pictures = new [] { newArt };
            }
        }

        public TagInformation ToTagInformation()
        {
            var info = new TagInformation();

            if (!string.IsNullOrEmpty(Album.Content))
            {
                info.Album.SetValue(Album.Content);
            }

            if (!string.IsNullOrEmpty(AlbumArtist.Content))
            {
                info.AlbumArtist.SetValue(AlbumArtist.Content);
            }

            if (!string.IsNullOrEmpty(Artist.Content))
            {
                info.Artist.SetValue(Artist.Content);
            }

            if (!string.IsNullOrEmpty(Title))
            {
                info.Title.SetValue(Title);
            }

            if (Year != null)
            {
                info.Year.SetValue((int?)Year.Content);
            }

            info.TrackNumber.SetValue(TrackNumber);
            info.TrackNumber.TrackCount = TrackCount;

            if (!string.IsNullOrEmpty(Genre.Content))
            {
                info.Genre.Type = Genre.Content;
            }

            if (AlbumArt != null)
            {
                info.AlbumArt.Type = PictureType.FrontCover;
                info.AlbumArt.MimeType = albumArt.Content.MimeType;
                info.AlbumArt.SetValue(albumArt.Content.Content);
            }

            return info;
        }


        public MultiInfo<string> Artist
        {
            get { return artist; }
            set { SetProperty(ref artist, value); }
        }

        public string Title
        {
            get { return title; }
            set { SetProperty(ref title, value); }
        }

        public MultiInfo<string> Album
        {
            get { return album; }
            set { SetProperty(ref album, value); }
        }

        public MultiInfo<string> AlbumArtist
        {
            get { return albumArtist; }
            set { SetProperty(ref albumArtist, value); }
        }

        public MultiInfo<uint?> Year
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

        public MultiInfo<ImageTag> AlbumArt
        {
            get { return albumArt; }
            set { SetProperty(ref albumArt, value); }
        }

        public MultiInfo<string> Genre
        {
            get { return genre; }
            set { SetProperty(ref genre, value); }
        }
    }
}
