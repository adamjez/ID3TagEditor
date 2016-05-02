using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TagEditor.Lib.ID3v2
{
    internal static class HelperMethods
    {
        public static int ParseSize(byte[] bytes)
        {
            var size = 0;
            // Every 7 bits in each bytes is ignored to not trigger 'false syncsignals'
            foreach (byte number in bytes)
            {
                size <<= 6;
                size += number & 0x7F;
            }
            return size;
        }
    }
}
