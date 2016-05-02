using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TagEditor.Lib.ID3v1
{
    public class TagInformation : ITagInformation
    {
        public StringBasicTag Title { get; private set; }
        public StringBasicTag Artist { get; private set; }
        public StringBasicTag Album { get; private set; }
        public StringNumberTag Year { get; private set; }
        public StringBasicTag Comment { get; private set; }
        public GenreTag Genre { get; private set; }
        public NumberTag TrackNumber { get; private set; }

        public TagInformation()
        {
            Title = new StringBasicTag(30);
            Artist = new StringBasicTag(30);
            Album = new StringBasicTag(30);
            Year = new StringNumberTag(4);
            Comment = new StringBasicTag(30);
            Genre = new GenreTag();
            TrackNumber = new NumberTag(1);
        }
    }
}
