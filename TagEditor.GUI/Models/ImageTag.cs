using System.Threading.Tasks;
using Windows.UI.Xaml.Media.Imaging;
using TagEditor.GUI.Utility;
using TagEditor.GUI.ViewModels;

namespace TagEditor.GUI.Models
{
    public class ImageTag : NotificationBase
    {
        private BitmapImage image;

        public BitmapImage Image
        {
            get { return image; }
            private set { SetProperty(ref image, value); }
        }

        public string MimeType { get; private set; }

        public byte[] Content { get; private set; }

        public async Task SetNewImage(byte[] content, string mimeType)
        {
            this.Content = content;
            this.MimeType = mimeType;
            Image = await content.ToImage();
        }
    }
}