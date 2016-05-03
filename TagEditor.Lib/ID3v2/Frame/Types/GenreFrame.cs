using TagEditor.Lib.ID3v1;

namespace TagEditor.Lib.ID3v2.Frame.Types
{
    internal class GenreFrame : TextFrame
    {
        public Genre.Type Type { get; set; }
        public GenreFrame() : base(FrameType.TCON)
        {
            Type = Genre.Type.None;
        }

        public override void Parse(byte[] bytes)
        {
            base.Parse(bytes);

            if (Content.Length >= 3 && Content[0] == '(')
            {
                Type = (Genre.Type)Content[1];
                Content = Content.Substring(3);
            }

            if (Type == Genre.Type.None)
            {
                Type = Genre.FromString(Content);
            }
        }
    }
}