using TagEditor.Core.ID3v2.Frame.Types;
using TagEditor.Core.Interfaces;

namespace TagEditor.Core.ID3v1
{
    public class ImageTag : ITag<byte[]>
    {
        public void SetValue(byte[] value)
        {
            Content = value;
        }

        public void SetValue(byte[] value, string mimeType)
        {
            MimeType = mimeType;
            SetValue(value);
        }


        public string Description { get; set; }

        public byte[] Content { get; private set;  }

        public string MimeType { get; set; }

        public PictureType Type { get; set; }
    }
}