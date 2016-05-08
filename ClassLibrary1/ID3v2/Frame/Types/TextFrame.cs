using System.Text;
using TagEditor.Core.Utility;

namespace TagEditor.Core.ID3v2.Frame.Types
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
    }
}