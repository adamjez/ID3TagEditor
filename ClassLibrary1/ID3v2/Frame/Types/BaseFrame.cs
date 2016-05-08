using System;
using System.Text;

namespace TagEditor.Core.ID3v2.Frame.Types
{
    internal abstract class BaseFrame
    {
        private const string baseCoding = "iso-8859-1";
        public FrameType Type { get; private set; }

        protected BaseFrame(FrameType type)
        {
            Type = type;
        }

        public abstract void Parse(byte[] bytes);

        public abstract byte[] Render();

        protected Encoding GetEncoding(byte encoding = 1)
        {
            Encoding encoder = null;
            if (encoding == 0)
            {
                // ISO-8859-1
                encoder = Encoding.GetEncoding(baseCoding);
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

        protected string ParseText(byte[] bytes)
        {
            return Encoding.GetEncoding(baseCoding).GetString(bytes);
        }

        protected byte[] RenderText(string text)
        {
            return Encoding.GetEncoding(baseCoding).GetBytes(text);
        }
    }
}
