using System;

namespace TagEditor.Core.ID3v2
{
    [Flags]
    internal enum HeaderFlags
    {
        Experimental = 32,
        Extended = 64,
        Unsynchronisation = 128
    }
}
