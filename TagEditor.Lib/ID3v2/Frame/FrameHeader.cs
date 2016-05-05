using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TagEditor.Lib.Utility;

namespace TagEditor.Lib.ID3v2.Frame
{
    internal class FrameHeader
    {
        public string FrameID { get; set; }
        public uint Size { get; set; }
        public FrameHeaderFlags1 Flags1 { get; set; }
        public FrameHeaderFlags2 Flags2 { get; set; }
        // Size + header size
        public uint FullSize => Size + 10;

        public static async Task<FrameHeader> Parse(byte[] bytes)
        {
            using (var ms = new MemoryStream(bytes))
            {
                if (ms.ReadByte() == 0)
                {
                    throw new EndOfFramesException();
                }
                // There is another frame, seek to begining
                ms.Seek(0, SeekOrigin.Begin);

                return new FrameHeader
                {
                    FrameID = Encoding.ASCII.GetString(await ms.ReadBytesAsync(4)),
                    Size = (await ms.ReadBytesAsync(4)).ToUInt(),
                    Flags1 = (FrameHeaderFlags1)await ms.ReadByteAsync(),
                    Flags2 = (FrameHeaderFlags2)await ms.ReadByteAsync()
                };

            }
        }

        public byte[] Render()
        {
            var id = Encoding.ASCII.GetBytes(FrameID);
            var size = BitConverter.GetBytes(Size).Reverse().ToArray();

            var buffer = new byte[10];
            Buffer.BlockCopy(id, 0, buffer, 0, 4);
            Buffer.BlockCopy(size, 0, buffer, 4, 4);
            buffer[8] = (byte) Flags1;
            buffer[9] = (byte)Flags2;

            return buffer;
        }
    }
}