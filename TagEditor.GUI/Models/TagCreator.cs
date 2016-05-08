using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Windows.Storage;
using TagEditor.Core.Common;
using TagLib;

namespace TagEditor.GUI.Models
{
    public static class TagCreator
    {
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
                        Artist = new MultiInfo<string>(info.Artist.Content),
                        Title = info.Title.Content,
                        Album = new MultiInfo<string>(info.Album.Content),
                        Year = new MultiInfo<uint?>((uint?)info.Year.Content),
                        TrackNumber = info.TrackNumber.Content,
                        TrackCount = info.TrackNumber.TrackCount,
                        Genre = new MultiInfo<string>(info.Genre.Type)
                    };

                    if (info.AlbumArt.Content != null)
                    {
                        tag.AlbumArt.Content = 
                            await ImageTag.CreateNewImage(info.AlbumArt.Content, info.AlbumArt.MimeType);
                    }

                    return tag;
                }
            }

            return new TagViewModel();
        }

        public static async Task<TagViewModel> LoadFromFiles(IEnumerable<StorageFile> files)
        {
            var editor = new Core.Common.TagEditor();

            var albums = new MultiInfo<string>();
            var artists = new MultiInfo<string>();
            var genres = new MultiInfo<string>();
            var years = new MultiInfo<uint?>();
            var albumArts = new MultiInfo<ImageTag>();
            foreach (var file in files)
            {
                using (var audioFile = new AudioFile(await file.OpenStreamForReadAsync(), true))
                {
                    var info = await editor.RetrieveTagsAsync(audioFile, TagType.ID3v2);

                    albums.AddUniqueToItems(info.Album.Content);
                    artists.AddUniqueToItems(info.Artist.Content);
                    genres.AddUniqueToItems(info.Genre.Type);

                    years.AddUniqueToItems((uint?)info.Year.Content);

                    var albumArt = await ImageTag.CreateNewImage(info.AlbumArt.Content, info.AlbumArt.MimeType);
                    albumArts.AddUniqueToItems(albumArt,
                        (tag1, tag2) => tag1.MimeType == tag2.MimeType && tag1.Content.SequenceEqual(tag2.Content));
                    //AddIfNotEmpty(info.Album.Content, albums);
                    //AddIfNotEmpty(info.Artist.Content, artists);
                    //AddIfNotEmpty(info.Genre.Type, genres);

                    //if (info.Year.Content != null && info.Year.Content > 0)
                    //{
                    //    years.AddUniqueToItems((uint?)info.Year.Content);
                    //}

                    //if (info.AlbumArt.Content != null)
                    //{
                    //    var albumArt = await ImageTag.CreateNewImage(info.AlbumArt.Content, info.AlbumArt.MimeType);
                    //    albumArts.AddUniqueToItems(albumArt, 
                    //        (tag1 ,tag2) => tag1.MimeType == tag2.MimeType && tag1.Content.SequenceEqual(tag2.Content));
                    //}
                }
            }

            return new TagViewModel
            {
                AlbumArt = albumArts.Prepare(),
                Artist = artists.Prepare(),
                Genre = genres.Prepare(),
                Album = albums.Prepare(),
                Year = years.Prepare()
            };
        }

        private static void AddIfNotEmpty(string content, MultiInfo<string> albums)
        {
            if (!string.IsNullOrEmpty(content))
            {
                albums.AddUniqueToItems(content);
            }
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
                    result.Album = new MultiInfo<string>(tags.Album);
                    result.Artist = new MultiInfo<string>(tags.AlbumArtists.FirstOrDefault());
                    result.Title = tags.Title;
                    result.Year = new MultiInfo<uint?>(tags.Year);
                    result.TrackNumber = tags.Track;
                    result.TrackCount = tags.TrackCount;
                    result.Genre = new MultiInfo<string>(tags.FirstGenre);

                    var art = tags.Pictures.FirstOrDefault(pic => pic.Type == PictureType.FrontCover);
                    if (art != null)
                    {
                        result.AlbumArt.Content = await ImageTag.CreateNewImage(art.Data.Data, art.MimeType);
                    }

                    return result;
                }
            }

            return new TagViewModel();
        }
    }
}