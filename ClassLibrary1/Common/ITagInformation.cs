using TagEditor.Core.ID3v1;

namespace TagEditor.Core.Common
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
        ImageTag AlbumArt { get; }
    }
}