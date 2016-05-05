using System;
using System.Linq;
using TagEditor.Lib.ID3v1;

namespace TagEditor.Lib.ID3v2.Frame.Types
{
    internal class GenreFrame : TextFrame
    {
        public Genre.Type GenreType { get; set; }
        public GenreFrame() : base(FrameType.TCON)
        {
            GenreType = Genre.Type.None;
        }

        public override void Parse(byte[] bytes)
        {
            base.Parse(bytes);

            if (Content.Length >= 3 && Content[0] == '(')
            {
                GenreType = (Genre.Type)Content[1];
                Content = Content.Substring(3);
            }

            if (GenreType == Genre.Type.None)
            {
                GenreType = Genre.FromString(Content);
            }
        }

        public override byte[] Render()
        {
            var buffer = base.Render();

            if (GenreType != Genre.Type.None)
            {
                var number = BitConverter.GetBytes((int)GenreType);

                var numberBytes = new byte[number.Length + 2];
                numberBytes[0] = (byte)'(';
                Buffer.BlockCopy(number, 0, numberBytes, 1, number.Length);
                numberBytes[numberBytes.Length - 1] = (byte)')';

                buffer = numberBytes.Concat(buffer).ToArray();
            }

            return buffer;
        }
    }
}