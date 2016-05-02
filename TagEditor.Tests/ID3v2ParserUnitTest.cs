using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TagEditor.Lib.Common;
using TagEditor.Lib.ID3v1;

namespace TagEditor.Tests
{
    [TestClass]
    public class ID3v2ParserUnitTest
    {
        [TestMethod]
        public async Task YearTest()
        {
            using (var file = new AudioFile())
            {
                file.Open("AudioFiles/test1.mp3");

                var editor = new Lib.Common.TagEditor();

                var info = await editor.RetrieveTagsAsync(file, TagType.ID3v2);

                Assert.AreEqual(2012, info.Year.Content);
            }
        }

        [TestMethod]
        public async Task GenreTest()
        {
            using (var file = new AudioFile())
            {
                file.Open("AudioFiles/test.mp3");

                var editor = new Lib.Common.TagEditor();

                var info = await editor.RetrieveTagsAsync(file, TagType.ID3v2);

                Assert.AreEqual(Genre.Type.Soundtrack, info.Genre.Content);
            }
        }

        [TestMethod]
        public async Task TitleTest()
        {
            using (var file = new AudioFile())
            {
                file.Open("AudioFiles/test.mp3");

                var editor = new Lib.Common.TagEditor();

                var info = await editor.RetrieveTagsAsync(file, TagType.ID3v2);

                Assert.AreEqual("Title", info.Title.Content);
            }
        }

        [TestMethod]
        public async Task ArtistTest()
        {
            using (var file = new AudioFile())
            {
                file.Open("AudioFiles/test.mp3");

                var editor = new Lib.Common.TagEditor();

                var info = await editor.RetrieveTagsAsync(file, TagType.ID3v2);

                Assert.AreEqual("�d�m J�", info.Artist.Content);
            }
        }

        [TestMethod]
        public async Task TrackNumberTest()
        {
            using (var file = new AudioFile())
            {
                file.Open("AudioFiles/test.mp3");

                var editor = new Lib.Common.TagEditor();

                var info = await editor.RetrieveTagsAsync(file, TagType.ID3v2);

                Assert.AreEqual((uint?)12, info.TrackNumber.Content);
            }
        }
    }
}