using System;
using System.Text;

namespace TagEditor.Lib.ID3v2.Frame.Types
{
    internal abstract class BaseFrame
    {
        public FrameType Type { get; private set; }

        protected BaseFrame(FrameType type)
        {
            Type = type;
        }

        public abstract void Parse(byte[] bytes);

        public abstract byte[] Render();

        protected Encoding GetEncoding(byte encoding = 0)
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

        protected string ParseText(byte[] bytes)
        {
            return Encoding.GetEncoding("iso-8859-1").GetString(bytes);
        }
    }
}
