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
            int pos = 0;

            var encoder = GetEncoding(bytes[pos++]);

            // Parsing MIME type
            var offset = bytes.SubArray(pos).IndexOf(new byte[] { 0x00 });

            MimeType = ParseText(bytes.SubArray(pos, offset));

            pos = offset + 2;
            if (!MimeType.Contains("image/"))
            {
                MimeType = "image/" + MimeType;
            }

            // Parsing Picture GenreType
            PictureType = (PictureType)bytes[pos++];

            // Parsing Description     
            var buffer = bytes.SubArray(pos);

            var delimiter = encoder.GetDelimiter();
            var index = buffer.IndexOf(delimiter) ;

            Description = ParseString(buffer.SubArray(0, index), encoder);

            Image = buffer.SubArray(index + delimiter.Length);
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