namespace TagEditor.Library.ID3v2
{
    internal static class HelperMethods
    {
        public static uint ParseSynchSize(byte[] bytes)
        {
            uint size = 0;
            // Every 8 bits in each bytes is ignored to not trigger 'false syncsignals'
            foreach (byte number in bytes)
            {
                size <<= 7;
                size |= (uint)(number & 0x7F);
            }
            return size;
        }
    }
}
