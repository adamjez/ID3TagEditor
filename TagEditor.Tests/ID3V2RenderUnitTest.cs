using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TagEditor.Core.Common;
using TagEditor.Core.ID3v1;
using TagEditor.Core.Utility;

namespace TagEditor.Tests
{
    [TestClass]
    public class ID3v2RenderUnitTest
    {
        [TestMethod]
        public async Task RenderArtistTest()
        {
            using (var file = new AudioFile(File.Open("AudioFiles/test1.mp3", FileMode.Open)))
            {
                var editor = new Core.Common.TagEditor();

                var info = new TagInformation();
                info.Artist.SetValue("Adam Ježek");

                await editor.SetTags(file, info, TagType.ID3v2);
            }

            using (var file = new AudioFile(File.Open("AudioFiles/test1.mp3", FileMode.Open)))
            {
                var editor = new Core.Common.TagEditor();

                var newInfo = await editor.RetrieveTagsAsync(file, TagType.ID3v2);

                Assert.AreEqual("Adam Ježek", newInfo.Artist.Content);
            }
        }

        [TestMethod]
        public async Task RenderTitleTest()
        {
            using (var file = new AudioFile(File.Open("AudioFiles/test1.mp3", FileMode.Open)))
            {
                var editor = new Core.Common.TagEditor();

                var info = new TagInformation();
                info.Title.SetValue("Tituležek");

                await editor.SetTags(file, info, TagType.ID3v2);
            }

            using (var file = new AudioFile(File.Open("AudioFiles/test1.mp3", FileMode.Open)))
            {
                var editor = new Core.Common.TagEditor();

                var newInfo = await editor.RetrieveTagsAsync(file, TagType.ID3v2);

                Assert.AreEqual("Tituležek", newInfo.Title.Content);
            }
        }

        [TestMethod]
        public async Task RenderGenreTest()
        {
            using (var file = new AudioFile(File.Open("AudioFiles/test1.mp3", FileMode.Open)))
            {
                var editor = new Core.Common.TagEditor();

                var info = new TagInformation();
                info.Genre.SetValue(Genre.Type.Bass);

                await editor.SetTags(file, info, TagType.ID3v2);
            }

            using (var file = new AudioFile(File.Open("AudioFiles/test1.mp3", FileMode.Open)))
            {
                var editor = new Core.Common.TagEditor();

                var newInfo = await editor.RetrieveTagsAsync(file, TagType.ID3v2);

                Assert.AreEqual(Genre.Type.Bass, newInfo.Genre.Content);
            }
        }

        [TestMethod]
        public async Task RenderYearTest()
        {
            using (var file = new AudioFile(File.Open("AudioFiles/test1.mp3", FileMode.Open)))
            {
                var editor = new Core.Common.TagEditor();

                var info = new TagInformation();
                info.Year.SetValue(2010);

                await editor.SetTags(file, info, TagType.ID3v2);
            }

            using (var file = new AudioFile(File.Open("AudioFiles/test1.mp3", FileMode.Open)))
            {
                var editor = new Core.Common.TagEditor();

                var newInfo = await editor.RetrieveTagsAsync(file, TagType.ID3v2);

                Assert.AreEqual(2010, newInfo.Year.Content);
            }
        }

        [TestMethod]
        public async Task RenderTrackNumberTest()
        {
            using (var file = new AudioFile(File.Open("AudioFiles/test1.mp3", FileMode.Open)))
            {
                var editor = new Core.Common.TagEditor();

                var info = new TagInformation();
                info.TrackNumber.SetValue(2);

                await editor.SetTags(file, info, TagType.ID3v2);
            }

            using (var file = new AudioFile(File.Open("AudioFiles/test1.mp3", FileMode.Open)))
            {
                var editor = new Core.Common.TagEditor();

                var newInfo = await editor.RetrieveTagsAsync(file, TagType.ID3v2);

                Assert.AreEqual((uint?)2, newInfo.TrackNumber.Content);
            }
        }

        [TestMethod]
        public async Task RenderAlbumArtistTest()
        {
            using (var file = new AudioFile(File.Open("AudioFiles/test1.mp3", FileMode.Open)))
            {
                var editor = new Core.Common.TagEditor();

                var info = new TagInformation();

                info.AlbumArtist.SetValue("Album Artist");
           
                await editor.SetTags(file, info, TagType.ID3v2);
                

            }

            using (var file = new AudioFile(File.Open("AudioFiles/test1.mp3", FileMode.Open)))
            {
                var editor = new Core.Common.TagEditor();

                var newInfo = await editor.RetrieveTagsAsync(file, TagType.ID3v2);

                Assert.AreEqual(newInfo.AlbumArtist.Content, "Album Artist");
            }
        }

        [TestMethod]
        public async Task RenderAlbumArtTest()
        {
            using (var file = new AudioFile(File.Open("AudioFiles/test1.mp3", FileMode.Open)))
            {
                var editor = new Core.Common.TagEditor();

                var info = new TagInformation();

                var bitmap = new Bitmap("ImageFiles/Adele.png");
                using(var ms =  new MemoryStream())
                {
                    var format = ImageFormat.Png;
                    bitmap.Save(ms, format);

                    info.AlbumArt.SetValue(ms.ToArray(), format.ToString());

                    await editor.SetTags(file, info, TagType.ID3v2);
                }

            }

            using (var file = new AudioFile(File.Open("AudioFiles/test1.mp3", FileMode.Open)))
            {
                var editor = new Core.Common.TagEditor();

                var newInfo = await editor.RetrieveTagsAsync(file, TagType.ID3v2);

                var bitmap = new Bitmap("ImageFiles/Adele.png");
                using (var ms = new MemoryStream())
                {
                    var format = ImageFormat.Png;
                    bitmap.Save(ms, format);

                    Assert.IsTrue(ms.ToArray().SequenceEqual(newInfo.AlbumArt.Content));
                }
            }
        }
    }
}