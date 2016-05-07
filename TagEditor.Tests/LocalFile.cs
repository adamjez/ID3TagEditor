using System.IO;
using TagEditor.Core.Common;

namespace TagEditor.Tests
{
    public class LocalFile : IFileAbstraction
    {
        public static Stream Load(string path)
        {
            return File.Open(path, FileMode.Open, FileAccess.ReadWrite);
        }

        public LocalFile(string path)
        {
            Stream = File.Open(path, FileMode.Open, FileAccess.ReadWrite);
        }
        public Stream Stream { get; set; }
        public void CloseStream(Stream stream)
        {
            Stream?.Dispose();
        }
    }
}
