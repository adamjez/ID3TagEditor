using System.Threading.Tasks;
using Microsoft.VisualStudio.TestPlatform.UnitTestFramework;
using TagEditor.Library.Common;
using TagEditor.Library.ID3v1;

namespace TagEditor.UnitTests
{
    [TestClass]
    public class ID3v1ParserUnitTest
    {
        [TestMethod]
        public async Task YearTest()
        {
            using (var file = new AudioFile())
            {
                file.Open("AudioFiles/test.mp3");

                var editor = new Library.Common.TagEditor();

                var info = await editor.RetrieveTagsAsync(file, TagType.ID3v1);

                Assert.AreEqual(2012, info.Year.Content);
            }
        }

        [TestMethod]
        public async Task GenreTest()
        {
            using (var file = new AudioFile())
            {
                file.Open("AudioFiles/test.mp3");

                var editor = new Library.Common.TagEditor();

                var info = await editor.RetrieveTagsAsync(file, TagType.ID3v1);

                Assert.AreEqual(Genre.Type.Soundtrack, info.Genre.Content);
            }
        }

        [TestMethod]
        public async Task TitleTest()
        {
            using (var file = new AudioFile())
            {
                file.Open("AudioFiles/test.mp3");

                var editor = new Library.Common.TagEditor();

                var info = await editor.RetrieveTagsAsync(file, TagType.ID3v1);

                Assert.AreEqual("Title", info.Title.Content);
            }
        }

        [TestMethod]
        public async Task ArtistTest()
        {
            using (var file = new AudioFile())
            {
                file.Open("AudioFiles/test.mp3");

                var editor = new Library.Common.TagEditor();

                var info = await editor.RetrieveTagsAsync(file, TagType.ID3v1);

                Assert.AreEqual("Ádám Jéž", info.Artist.Content);
            }
        }

        [TestMethod]
        public async Task TrackNumberTest()
        {
            using (var file = new AudioFile())
            {
                file.Open("AudioFiles/test.mp3");

                var editor = new Library.Common.TagEditor();

                var info = await editor.RetrieveTagsAsync(file, TagType.ID3v1);

                Assert.AreEqual((uint?)12, info.TrackNumber.Content);
            }
        }
    }
}
