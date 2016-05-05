using TagEditor.Lib.ID3v1;

namespace TagEditor.Lib.Common
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