﻿using System;
using TagEditor.Lib.ID3v2.Frame.Types;

namespace TagEditor.Lib.ID3v2.Frame
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

            throw new NotImplementedException();
        }
    }
}
