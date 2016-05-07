using System.IO;
using System.Threading.Tasks;
using TagEditor.Library.Utility;

namespace TagEditor.Library.ID3v2
{
    internal class ExtendedHeader
    {
        public uint Size { get; set; }

        public ExtendedHeaderFlags Flags { get; set; }

        public uint PaddingSize { get; set; }

        public static async Task<ExtendedHeader> Parse(byte[] bytes)
        {
            using (var ms = new MemoryStream(bytes))
            {
                return new ExtendedHeader
                {
                    Size = HelperMethods.ParseSynchSize(await ms.ReadBytesAsync(4)),
                    Flags = (ExtendedHeaderFlags) (await ms.ReadBytesAsync(2))[0],
                    PaddingSize = HelperMethods.ParseSynchSize(await ms.ReadBytesAsync(4))
                };
            }
        }
    }
}