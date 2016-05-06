using System;
using System.Diagnostics.CodeAnalysis;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Net.Mime;
using System.Text;
using TagEditor.Lib.Utility;

namespace TagEditor.Lib.ID3v2.Frame.Types
{
    internal class AttachedPictureFrame : BaseFrame
    {
        private readonly ImageFormat defaultFormat = ImageFormat.Png;
        public PictureType PictureType { get; set; }
        public string Description { get; set; }
        public Image Image { get; set; }
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

            index = buffer.IndexOf(encoder.GetDelimiter());

            Description = encoder.GetString(buffer.SubArray(0, index));

            var pictureData = buffer.SubArray(Description.Length + 1);
            using (MemoryStream ms = new MemoryStream(pictureData))
            {
                Image = Image.FromStream(ms);
            }
        }

        public override byte[] Render()
        {
            // ToDo: make indexing with some nicer way
            var encoder = GetEncoding();

            using (Stream ms = new MemoryStream())
            {
                Image.Save(ms, defaultFormat);

                var mimeType = RenderText(defaultFormat.ToString());

                var buffer = new byte[500];
                buffer[0] = encoder.GetByte();
                Buffer.BlockCopy(mimeType, 0, buffer, 1, mimeType.Length);
                buffer[mimeType.Length + 1] = 0x00;
                buffer[mimeType.Length + 2] = (byte) PictureType;

                var description = encoder.GetBytes(Description);
                Buffer.BlockCopy(description, 0, buffer, mimeType.Length + 3, description.Length);
                var delimiter = encoder.GetDelimiter();
                Buffer.BlockCopy(delimiter, 0, buffer, mimeType.Length + description.Length + 3, delimiter.Length);

                // Image
                ms.Seek(0, SeekOrigin.Begin);
                ms.Write(buffer, mimeType.Length + description.Length + delimiter.Length + 3, (int)ms.Length);

                return buffer;;
            }
        }
    }
}