using System;
using System.Text;
using TagEditor.Lib.Utility;

namespace TagEditor.Lib.ID3v2.Frame.Types
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

        protected Encoding GetEncoding(byte encoding)
        {
            Encoding encoder = null;
            if (encoding == 0)
            {
                // ISO-8859-1
                encoder = Encoding.GetEncoding("iso-8859-1");
            }
            else if (encoding == 1)
            {
                // Unicode
                encoder = Encoding.Unicode;
            }
            else
            {
                throw new ArgumentException("Unknown type of encoding text frame");
            }
            return encoder;
        }
    }
}