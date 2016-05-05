using System.Drawing;
using System.IO;
using System.Net.Mime;
using System.Text;
using TagEditor.Lib.Utility;

namespace TagEditor.Lib.ID3v2.Frame.Types
{
    internal class AttachedPictureFrame : BaseFrame
    {
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
            throw new System.NotImplementedException();
        }
    }
}