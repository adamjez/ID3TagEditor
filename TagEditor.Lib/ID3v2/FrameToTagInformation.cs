using System;
using TagEditor.Lib.ID3v1;
using TagEditor.Lib.ID3v2.Frame.Types;

namespace TagEditor.Lib.ID3v2
{
    internal static class FrameToTagInformation
    {
        public static void Fill(BaseFrame frame, ITagInformation tag)
        {
            switch (frame.Type)
            {
                case FrameType.TIT2:
                    tag.Title.SetValue(((TextFrame)frame).Content);
                    break;
                case FrameType.TPE1:
                case FrameType.TOPE:
                    tag.Artist.SetValue(((TextFrame)frame).Content);
                    break;
                case FrameType.TOAL:
                case FrameType.TALB:
                    tag.Album.SetValue(((TextFrame)frame).Content);
                    break;
                case FrameType.TYER:
                    tag.Year.SetValue(Int32.Parse(((TextFrame)frame).Content));
                    break;
                case FrameType.TRCK:
                    tag.TrackNumber.SetValue(UInt32.Parse(((TextFrame)frame).Content));
                    break;
                case FrameType.TCON:
                    tag.Genre.SetValue(((GenreFrame)frame).Type);
                    break;
                default:
                    break;
            }

        }

    }
}
