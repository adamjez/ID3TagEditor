using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TagEditor.Lib.Common;
using TagEditor.Lib.ID3v1;
using TagEditor.Lib.ID3v2;

namespace TagEditor.Tests
{
    [TestClass]
    public class ID3V2RenderUnitTest
    {
        [TestMethod]
        public async Task RenderArtistTest()
        {
            using (var file = new AudioFile())
            {
                file.Open("AudioFiles/test1.mp3", false);

                var editor = new Lib.Common.TagEditor();

                var info = new TagInformationV2();
                info.Artist.SetValue("Adam Ježek");

                await editor.SetTags(file, info, TagType.ID3v2);
            }

            using (var file = new AudioFile())
            {
                file.Open("AudioFiles/test1.mp3");

                var editor = new Lib.Common.TagEditor();

                var newInfo = await editor.RetrieveTagsAsync(file, TagType.ID3v2);

                Assert.AreEqual("Adam Ježek", newInfo.Artist.Content);
            }

            //using (var file = new AudioFile())
            //{
            //    var editor = new Lib.Common.TagEditor();

            //    file.Open("AudioFiles/test1.mp3", false);


            //    await editor.RemoveTags(file, TagType.ID3v1);
            //}
        }

        [TestMethod]
        public async Task RenderTitleTest()
        {
            using (var file = new AudioFile())
            {
                file.Open("AudioFiles/test1.mp3", false);

                var editor = new Lib.Common.TagEditor();

                var info = new TagInformationV2();
                info.Title.SetValue("Tituležek");

                await editor.SetTags(file, info, TagType.ID3v2);
            }

            using (var file = new AudioFile())
            {
                file.Open("AudioFiles/test1.mp3");

                var editor = new Lib.Common.TagEditor();

                var newInfo = await editor.RetrieveTagsAsync(file, TagType.ID3v2);

                Assert.AreEqual("Tituležek", newInfo.Title.Content);
            }
        }

        [TestMethod]
        public async Task RenderGenreTest()
        {
            using (var file = new AudioFile())
            {
                file.Open("AudioFiles/test1.mp3", false);

                var editor = new Lib.Common.TagEditor();

                var info = new TagInformationV2();
                info.Genre.SetValue(Genre.Type.Bass);

                await editor.SetTags(file, info, TagType.ID3v2);
            }

            using (var file = new AudioFile())
            {
                file.Open("AudioFiles/test1.mp3");

                var editor = new Lib.Common.TagEditor();

                var newInfo = await editor.RetrieveTagsAsync(file, TagType.ID3v2);

                Assert.AreEqual(Genre.Type.Bass, newInfo.Genre.Content);
            }
        }

        [TestMethod]
        public async Task RenderYearTest()
        {
            using (var file = new AudioFile())
            {
                file.Open("AudioFiles/test1.mp3", false);

                var editor = new Lib.Common.TagEditor();

                var info = new TagInformationV2();
                info.Year.SetValue(2010);

                await editor.SetTags(file, info, TagType.ID3v2);
            }

            using (var file = new AudioFile())
            {
                file.Open("AudioFiles/test1.mp3");

                var editor = new Lib.Common.TagEditor();

                var newInfo = await editor.RetrieveTagsAsync(file, TagType.ID3v2);

                Assert.AreEqual(2010, newInfo.Year.Content);
            }
        }

        [TestMethod]
        public async Task RenderTrackNumberTest()
        {
            using (var file = new AudioFile())
            {
                file.Open("AudioFiles/test1.mp3", false);

                var editor = new Lib.Common.TagEditor();

                var info = new TagInformationV2();
                info.TrackNumber.SetValue(2);

                await editor.SetTags(file, info, TagType.ID3v2);
            }

            using (var file = new AudioFile())
            {
                file.Open("AudioFiles/test1.mp3");

                var editor = new Lib.Common.TagEditor();

                var newInfo = await editor.RetrieveTagsAsync(file, TagType.ID3v2);

                Assert.AreEqual((uint?)2, newInfo.TrackNumber.Content);
            }
        }
    }
}