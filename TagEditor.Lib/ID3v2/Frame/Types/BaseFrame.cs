using System;
using System.Text;

namespace TagEditor.Lib.ID3v2.Frame.Types
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
