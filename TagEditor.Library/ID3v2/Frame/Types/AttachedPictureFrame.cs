using System;
using System.Drawing.Imaging;
using System.IO;
using Windows.Storage.Streams;
using Windows.UI.Xaml.Media.Imaging;
using TagEditor.Library.Utility;
using Buffer = System.Buffer;

namespace TagEditor.Library.ID3v2.Frame.Types
{
    internal class AttachedPictureFrame : BaseFrame
    {
        private readonly ImageFormat defaultFormat = ImageFormat.Jpeg;
        public PictureType PictureType { get; set; }
        public string Description { get; set; }
        public BitmapImage Image { get; set; }
        public AttachedPictureFrame() : base(FrameType.APIC)
        {
        }

        public override void Parse(byte[] bytes)
        {
            var encoder = GetEncoding(bytes[0]);

            // Parsing MIME type
            var index = bytes.SubArray(1).IndexOf(new byte[]{0x00});

            var mimeType = ParseText(bytes.SubArray(1, index));

            // Parsing Picture GenreType
            PictureType = (PictureType) bytes[index + 2];

            // Parsing Description     
            var buffer = bytes.SubArray(index + 3);

            var delimiter = encoder.GetDelimiter();
            index = buffer.IndexOf(delimiter);

            Description = encoder.GetString(buffer.SubArray(0, index));

            var pictureData = buffer.SubArray(Description.Length + delimiter.Length);

            Image = new BitmapImage();
            using (IRandomAccessStream ms = new MemoryStream(pictureData).AsRandomAccessStream())
            {
                Image.SetSource(ms);
            }
            
        }

        public override byte[] Render()
        {
            // ToDo: make indexing with some nicer way
            var encoder = GetEncoding();

            using (var ms = new MemoryStream())
            {
                AsyncHelpers.RunSync(() => Image.ToStream(ms.AsRandomAccessStream(), defaultFormat.Guid));

                var mimeType = RenderText(defaultFormat.ToString().ToLower());

                Byte[] description = null;
                if (!string.IsNullOrEmpty(Description))
                {
                    description = encoder.GetBytes(Description);
                }

                var delimiter = encoder.GetDelimiter();
                var resultSize = 3 + mimeType.Length + (description?.Length ?? 0)  + delimiter.Length + ms.Length;

                var buffer = new byte[resultSize];

                buffer[0] = encoder.GetByte();
                Buffer.BlockCopy(mimeType, 0, buffer, 1, mimeType.Length);
                buffer[mimeType.Length + 1] = 0x00;
                buffer[mimeType.Length + 2] = (byte) PictureType;

                // Description with delimiter       
                if (description != null)
                {
                    Buffer.BlockCopy(description, 0, buffer, mimeType.Length + 3, description.Length);
                }
                Buffer.BlockCopy(delimiter, 0, buffer, mimeType.Length + (description?.Length ?? 0) + 3, delimiter.Length);

                // Image
                ms.Seek(0, SeekOrigin.Begin);
                ms.Read(buffer, mimeType.Length + (description?.Length ?? 0) + delimiter.Length + 3, (int)ms.Length);
     
                return buffer;
            }
        }
    }
}