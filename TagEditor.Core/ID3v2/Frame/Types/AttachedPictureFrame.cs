using System;
using System.IO;
using TagEditor.Core.Utility;
using Buffer = System.Buffer;

namespace TagEditor.Core.ID3v2.Frame.Types
{
    internal class AttachedPictureFrame : BaseFrame
    {
        public PictureType PictureType { get; set; }
        public string Description { get; set; }
        public Byte[] Image { get; set; }
        public string MimeType { get; set; }

        public AttachedPictureFrame() : base(FrameType.APIC)
        {
        }

        public override void Parse(byte[] bytes)
        {
            var encoder = GetEncoding(bytes[0]);

            // Parsing MIME type
            var index = bytes.SubArray(1).IndexOf(new byte[] { 0x00 });

            MimeType = ParseText(bytes.SubArray(1, index));

            if (!MimeType.Contains("image/"))
            {
                MimeType = "image/" + MimeType;
            }

            // Parsing Picture GenreType
            PictureType = (PictureType)bytes[index + 2];

            // Parsing Description     
            var buffer = bytes.SubArray(index + 3);

            var delimiter = encoder.GetDelimiter();
            index = buffer.IndexOf(delimiter);

            Description = encoder.GetString(buffer.SubArray(0, index));

            Image = buffer.SubArray(Description.Length + delimiter.Length);
        }

        public override byte[] Render()
        {
            // ToDo: make indexing with some nicer way
            var encoder = GetEncoding();

            var mimeType = RenderText(MimeType.ToLower());

            Byte[] description = null;
            if (!string.IsNullOrEmpty(Description))
            {
                description = encoder.GetBytes(Description);
            }

            var delimiter = encoder.GetDelimiter();
            var resultSize = 3 + mimeType.Length + (description?.Length ?? 0) + delimiter.Length + Image.Length;

            var buffer = new byte[resultSize];

            buffer[0] = encoder.GetByte();
            Buffer.BlockCopy(mimeType, 0, buffer, 1, mimeType.Length);
            buffer[mimeType.Length + 1] = 0x00;
            buffer[mimeType.Length + 2] = (byte)PictureType;

            // Description with delimiter       
            if (description != null)
            {
                Buffer.BlockCopy(description, 0, buffer, mimeType.Length + 3, description.Length);
            }
            Buffer.BlockCopy(delimiter, 0, buffer, mimeType.Length + (description?.Length ?? 0) + 3, delimiter.Length);

            // Image
            Buffer.BlockCopy(Image, 0, buffer, mimeType.Length + (description?.Length ?? 0) + delimiter.Length + 3, Image.Length);

            return buffer;
        }

    }
}