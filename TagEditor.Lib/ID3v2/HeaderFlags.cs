using System;

namespace TagEditor.Lib.ID3v2
{
    [Flags]
    internal enum HeaderFlags
    {
        Experimental = 32,
        Extended = 64,
        Unsynchronisation = 128
    }
}
