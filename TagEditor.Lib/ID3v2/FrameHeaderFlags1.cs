using System;

namespace TagEditor.Lib.ID3v2
{
    [Flags]
    internal enum FrameHeaderFlags1
    {
        ReadOnly = 32,
        FileAlterPreservation = 64,
        TagAlterPreservation = 128
    }
}