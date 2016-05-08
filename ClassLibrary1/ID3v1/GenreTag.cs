using System;
using System.Linq;

namespace TagEditor.Core.ID3v1
{
    public class GenreTag : BasicTag<Genre.Type>
    {
        public GenreTag()
            : base(1)
        {
            Content = Genre.Type.None;
        }

        public override byte[] Render(int capacity = -1)
        {
            return new[] { (byte)Content };
        }

        public override void Parse(byte[] content)
        {
            if(content.Length < 1 )
                throw new ArgumentException(nameof(content));
            SetValue((Genre.Type)content.First());
        }

        public override bool Validate(Genre.Type val)
        {
            return true;
        }
    }
}