using Windows.UI.Xaml.Media.Imaging;
using TagEditor.Library.Interfaces;

namespace TagEditor.Library.ID3v1
{
    public class ImageTag : ITag<BitmapImage>
    {
        public void SetValue(BitmapImage value)
        {
            Content = value;
        }

        public string Description { get; set; }

        public BitmapImage Content { get; private set;  }
    }
}