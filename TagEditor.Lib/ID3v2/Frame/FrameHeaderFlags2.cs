using System;

namespace TagEditor.Lib.ID3v2.Frame
{
    [Flags]
    internal enum FrameHeaderFlags2
    {
        GroupingIdentity = 32,
        Encryption = 64,
        Compression = 128
    }
}