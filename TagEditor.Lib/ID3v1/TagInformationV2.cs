namespace TagEditor.Lib.ID3v1
{
    public class TagInformationV2 : ITagInformation
    {
        public StringBasicTag Album { get; set; }
        public StringBasicTag Artist { get; set; }
        public StringBasicTag Comment { get; set; }
        public GenreTag Genre { get; set; }
        public StringNumberTag Year { get; set; }
        public StringBasicTag Title { get; set; }
        public NumberTag TrackNumber { get; set; }

        public TagInformationV2()
        {
            Title = new StringBasicTag();
            Artist = new StringBasicTag();
            Album = new StringBasicTag();
            Year = new StringNumberTag(4);
            Comment = new StringBasicTag();
            Genre = new GenreTag();
            TrackNumber = new NumberTag(1);
        }
    }
}