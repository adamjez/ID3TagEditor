using System.Diagnostics;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using TagEditor.Lib.Utility;

namespace TagEditor.Lib.ID3v2
{
    internal class FrameHeader
    {
        public string FrameID { get; set; }
        public int Size { get; set; }
        public FrameHeaderFlags1 Flags1 { get; set; }
        public FrameHeaderFlags2 Flags2 { get; set; }

        public static async Task<FrameHeader> Parse(byte[] bytes)
        {
            using (var ms = new MemoryStream(bytes))
            {
                return new FrameHeader
                {
                    FrameID = Encoding.ASCII.GetString(await ms.ReadBytesAsync(4)),
                    Size = HelperMethods.ParseSize(await ms.ReadBytesAsync(4)),
                    Flags1 = (FrameHeaderFlags1) await ms.ReadByteAsync(),
                    Flags2 = (FrameHeaderFlags2) await ms.ReadByteAsync()
                };
            }
        }
    }
}