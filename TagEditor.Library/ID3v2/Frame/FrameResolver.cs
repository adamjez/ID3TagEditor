using System;
using TagEditor.Library.ID3v2.Frame.Types;

namespace TagEditor.Library.ID3v2.Frame
{
    internal static class FrameResolver
    {
        public static BaseFrame Resolve(FrameType type)
        {
            if (type.ToString().StartsWith("T"))
            {
                switch (type)
                {
                    case FrameType.TCON:
                        return new GenreFrame();
                    default:
                        return new TextFrame(type);
                }
            }
            else if (type == FrameType.COMM)
            {
                return new CommentFrame();
            }
            else if (type == FrameType.APIC)
            {
                return new AttachedPictureFrame();
            }

            throw new NotImplementedException();
        }
    }
}
