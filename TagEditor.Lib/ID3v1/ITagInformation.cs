namespace TagEditor.Lib.ID3v1
{
    public interface ITagInformation
    {
        StringBasicTag Album { get; }
        StringBasicTag Artist { get; }
        StringBasicTag Comment { get; }
        GenreTag Genre { get; }
        StringNumberTag Year { get; }
        StringBasicTag Title { get; }
        NumberTag TrackNumber { get; }

    }
}