using System.Threading.Tasks;
using Windows.UI.Xaml.Media.Imaging;
using TagEditor.GUI.Utility;
using TagEditor.GUI.ViewModels;

namespace TagEditor.GUI.Models
{
    public class ImageTag 
    {
        public BitmapImage Image { get; set; }

        public string MimeType { get; private set; }

        public byte[] Content { get; private set; }

        public static async Task<ImageTag> CreateNewImage(byte[] content, string mimeType)
        {
            return new ImageTag()
            {
                Content = content,
                MimeType = mimeType,
                Image = await content.ToImage(),
            };
        }
    }
}