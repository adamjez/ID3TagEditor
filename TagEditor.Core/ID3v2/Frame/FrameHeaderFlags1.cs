using System;

namespace TagEditor.Core.ID3v2.Frame
{
    [Flags]
    internal enum FrameHeaderFlags1
    {
        ReadOnly = 32,
        FileAlterPreservation = 64,
        TagAlterPreservation = 128
    }
}