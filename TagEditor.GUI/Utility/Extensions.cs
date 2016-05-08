using System;
using System.IO;
using System.Threading.Tasks;
using Windows.Graphics.Imaging;
using Windows.UI.Xaml.Media.Imaging;

namespace TagEditor.GUI.Utility
{
    public static class Extensions
    {
        public static async Task<BitmapImage> ToImage(this byte[] bytes)
        {
            if (bytes != null && bytes.Length > 0)
            {
                using (var ms = new MemoryStream(bytes))
                {
                    var image = new BitmapImage();

                    await image.SetSourceAsync(ms.AsRandomAccessStream());
                    return image;
                }
            }

            return null;
        }
    }
}
