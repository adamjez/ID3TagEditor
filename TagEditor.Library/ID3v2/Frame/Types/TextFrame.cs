using System.Text;
using TagEditor.Library.Utility;

namespace TagEditor.Library.ID3v2.Frame.Types
{
    internal class TextFrame : BaseFrame
    {
        public string Content { get; set; }

        public TextFrame(FrameType type) : base(type)
        {
        }

        public override void Parse(byte[] bytes)
        {
            var encoder = GetEncoding(bytes[0]);

            var newArray = bytes.SubArray(1, bytes.Length - 1);

            Content = ParseString(newArray, encoder);
        }

        public override byte[] Render()
        {
            var encoding = GetEncoding();

            var buffer = encoding.GetBytes(Content);

            byte[] newBuffer = new byte[buffer.Length + 1];

            newBuffer[0] = encoding.GetByte();
            buffer.CopyTo(newBuffer, 1);

            return newBuffer;
        }

        protected string ParseString(byte[] byteArray, Encoding encoder)
        {
            var content = encoder.GetString(byteArray);

            // Unicode strings must begin with the Unicode BOM 
            // ($FF FE or $FE FF) to identify the byte order.
            if (content.Length != 0 && (content[0] == 0xfffe || content[0] == 0xfeff))
            {
                content = content.Substring(1);
            }

            var nullIndex = content.IndexOf('\x00');
            if (nullIndex >= 0)
            {
                content = content.Substring(0, nullIndex);
            }

            return content;
        }
    }
}