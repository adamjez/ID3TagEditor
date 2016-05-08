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
                        tag.AlbumArt.Content = new ImageTag();
                        await tag.AlbumArt.Content.SetNewImage(info.AlbumArt.Content, info.AlbumArt.MimeType);
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

                    AddIfNotEmpty(info.Album.Content, albums);
                    AddIfNotEmpty(info.Artist.Content, artists);
                    AddIfNotEmpty(info.Genre.Type, genres);

                    if (info.Year.Content != null && info.Year.Content > 0)
                    {
                        years.SourceItems.Add((uint?)info.Year.Content);
                    }

                    if (info.AlbumArt.Content != null)
                    {
                        var albumArt = new ImageTag();
                        await albumArt.SetNewImage(info.AlbumArt.Content, info.AlbumArt.MimeType);
                        albumArts.SourceItems.Add(albumArt);
                    }
                }
            }

            return new TagViewModel
            {
                AlbumArt = albumArts,
                Artist = artists,
                Genre = genres,
                Year = years
            };
        }

        private static void AddIfNotEmpty(string content, MultiInfo<string> albums)
        {
            if (!string.IsNullOrEmpty(content))
            {
                albums.SourceItems.Add(content);
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

                    var art = tags.Pictures.FirstOrDefault(pic => pic.Type == TagLib.PictureType.FrontCover);
                    if (art != null)
                    {
                        await result.AlbumArt.Content.SetNewImage(art.Data.Data, art.MimeType);
                    }

                    return result;
                }
            }

            return new TagViewModel();
        }
    }
}