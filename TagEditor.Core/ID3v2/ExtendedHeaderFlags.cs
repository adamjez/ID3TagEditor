using System;

namespace TagEditor.Core.ID3v2
{
    [Flags]
    internal enum ExtendedHeaderFlags
    {
        CRCDataPresent = 128
    }
}