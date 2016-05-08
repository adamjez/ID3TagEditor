using System.IO;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TagEditor.Core.Common;
using TagEditor.Core.ID3v1;

namespace TagEditor.Tests
{
    [TestClass]
    public class ID3v2ParserUnitTest
    {
        [TestMethod]
        public async Task YearTest()
        {
            using (var file = new AudioFile(File.Open("AudioFiles/test.mp3", FileMode.Open)))
            {
                var editor = new Core.Common.TagEditor();

                var info = await editor.RetrieveTagsAsync(file, TagType.ID3v2);

                Assert.AreEqual(2012, info.Year.Content);
            }
        }

        [TestMethod]
        public async Task GenreTest()
        {
            using (var file = new AudioFile(File.Open("AudioFiles/test.mp3", FileMode.Open)))
            {
                var editor = new Core.Common.TagEditor();

                var info = await editor.RetrieveTagsAsync(file, TagType.ID3v2);

                Assert.AreEqual(Genre.Type.Soundtrack, info.Genre.Content);
            }
        }

        [TestMethod]
        public async Task TitleTest()
        {
            using (var file = new AudioFile(File.Open("AudioFiles/test.mp3", FileMode.Open)))
            {
                var editor = new Core.Common.TagEditor();

                var info = await editor.RetrieveTagsAsync(file, TagType.ID3v2);

                Assert.AreEqual("Title", info.Title.Content);
            }
        }

        [TestMethod]
        public async Task ArtistTest()
        {
            using (var file = new AudioFile(File.Open("AudioFiles/test.mp3", FileMode.Open)))
            {
                var editor = new Core.Common.TagEditor();

                var info = await editor.RetrieveTagsAsync(file, TagType.ID3v2);

                Assert.AreEqual("MP3 artist", info.Artist.Content);
            }
        }

        [TestMethod]
        public async Task TrackNumberTest()
        {
            using (var file = new AudioFile(File.Open("AudioFiles/test.mp3", FileMode.Open)))
            {
                var editor = new Core.Common.TagEditor();

                var info = await editor.RetrieveTagsAsync(file, TagType.ID3v2);

                Assert.AreEqual((uint?)12, info.TrackNumber.Content);
            }
        }

        [TestMethod]
        public async Task AlbumTest()
        {
            using (var file = new AudioFile(File.Open("AudioFiles/test.mp3", FileMode.Open)))
            {
                var editor = new Core.Common.TagEditor();

                var info = await editor.RetrieveTagsAsync(file, TagType.ID3v2);

                Assert.AreEqual(info.Album.Content, "Albumík");
            }
        }

        [TestMethod]
        public async Task AlbumArtistTest()
        {
            using (var file = new AudioFile(File.Open("AudioFiles/test.mp3", FileMode.Open)))
            {
                var editor = new Core.Common.TagEditor();

                var info = await editor.RetrieveTagsAsync(file, TagType.ID3v2);

                Assert.AreEqual(info.AlbumArtist.Content, "Petr Pán");
            }
        }
    }
}