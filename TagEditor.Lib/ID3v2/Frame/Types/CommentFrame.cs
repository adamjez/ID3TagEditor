using System.Linq;
using System.Text;
using TagEditor.Lib.Utility;

namespace TagEditor.Lib.ID3v2.Frame.Types
{
    internal class CommentFrame : TextFrame
    {
        public string Language { get; set; }
        public string Description { get; set; }
        public CommentFrame() : base(FrameType.COMM)
        {
        }

        public override void Parse(byte[] bytes)
        {
            var encoding = GetEncoding(bytes[0]);

            Language = Encoding.ASCII.GetString(bytes.SubArray(1, 3));

            var content = encoding.GetString(bytes.SubArray(4, bytes.Length - 4));

            var result = content.Split('\0');

            if (result.Length == 2)
            {
                Description = result[0];
                Content = result[1];
            }
        }
    }
}