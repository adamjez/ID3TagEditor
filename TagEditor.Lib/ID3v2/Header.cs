using System.Diagnostics;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using TagEditor.Lib.Utility;

namespace TagEditor.Lib.ID3v2
{
    internal class Header
    {
        private const string tag = "ID3";

        public int MajorVersion { get; set; }
        public int RevisionVersion { get; set; }
        public int Size { get; set; }
        public HeaderFlags Flags { get; set; }

        public static async Task<Header> Parse(byte[] bytes)
        {
            using (var ms = new MemoryStream(bytes))
            {
                var header = new Header();

                var tagString = Encoding.ASCII.GetString(await ms.ReadBytesAsync(3));

                if (tagString != tag)
                {
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

                header.Size = 4 * HelperMethods.ParseSize(await ms.ReadBytesAsync(4));

                return header;
            }
        }
    }
}