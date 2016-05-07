using System.Diagnostics;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using TagEditor.Core.Utility;

namespace TagEditor.Core.ID3v2
{
    internal class Header
    {
        private const string tag = "ID3";

        public int MajorVersion { get; set; }
        public int RevisionVersion { get; set; }
        public uint Size { get; set; }
        public HeaderFlags Flags { get; set; }


        public static async Task<Header> Parse(byte[] bytes)
        {
            using (var ms = new MemoryStream(bytes))
            {
                var header = new Header();

                var tagString = Encoding.ASCII.GetString(await ms.ReadBytesAsync(3));

                if (tagString != tag)
                {
                    Debug.WriteLine("ID3 tag not presented.");
                    return null;
                }

                header.MajorVersion = await ms.ReadByteAsync();
                header.RevisionVersion = await ms.ReadByteAsync();

                Debug.WriteLine("Parsind ID3v2 tag");
                Debug.WriteLine($"major version: {header.MajorVersion} " +
                                $"revision version: {header.RevisionVersion}");

                var flagNumber = await ms.ReadByteAsync();
                if ((flagNumber & 0xE0) != 0)
                {
                    Debug.WriteLine("File contains unsupported flags.");
                    return null;
                }
                header.Flags = (HeaderFlags)flagNumber;
                header.Size = HelperMethods.ParseSynchSize(await ms.ReadBytesAsync(4));

                return header;
            }
        }

        public byte[] Render()
        {
            var buffer = new byte[10];

            // Tag
            buffer[0] = (byte)'I';
            buffer[1] = (byte)'D';
            buffer[2] = (byte)'3';

            // Major Version
            buffer[3] = 3;
            // Revision Version
            buffer[4] = 0;

            // Flags
            buffer[5] = (byte)Flags;

            // Size
            buffer[6] = (byte)((Size >> 21) & 0x7F);
            buffer[7] = (byte)((Size >> 14) & 0x7F);
            buffer[8] = (byte)((Size >> 7) & 0x7F);
            buffer[9] = (byte)(Size & 0x7F);

            return buffer;
        }
    }
}