using System;
using TagEditor.Core.ID3v1;

namespace TagEditor.Core.ID3v2.Frame.Types
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

            if (Content[0] == '(')
            {
                var index = Content.IndexOf(")", StringComparison.Ordinal);
                GenreType = (Genre.Type)uint.Parse(Content.Substring(1, index - 1));
                Content = Content.Substring(index + 1);
            }

            if (GenreType == Genre.Type.None)
            {
                GenreType = Genre.FromString(Content);
            }
        }

        public override byte[] Render()
        {
            if (string.IsNullOrEmpty(Content))
            {
                Content = GenreType.ToString();
            }

            if (GenreType != Genre.Type.None)
            {
                Content = '(' + ((int) GenreType).ToString() + ')'
                          + Content;
            }

            return base.Render();
        }
    }
}