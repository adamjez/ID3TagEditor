using System;
using System.IO;
using System.Threading.Tasks;
using TagEditor.Lib.Interfaces;

namespace TagEditor.Lib.Common
{
    public class AudioFile : IFile, IDisposable
    {
        private FileStream fileStream;

        public void Open(string path, bool readOnly = true)
        {
            var accessLevel = readOnly ? FileAccess.Read : FileAccess.ReadWrite;
            fileStream = File.Open(path, FileMode.Open, accessLevel);
        }

        public async Task WriteAsync(byte[] content, int offset, bool reverseDirection = false)
        {
            if(!fileStream.CanWrite)
                throw new InvalidOperationException("Cannot write into file opened for only reading");

            if (reverseDirection)
                offset = (int) (fileStream.Length - offset);
            
            fileStream.Seek(offset, SeekOrigin.Begin);
            await fileStream.WriteAsync(content, 0, content.Length);
        }

        public async Task<byte[]> ReadAsync(int lastNBytes)
        {
            if (fileStream.Length < lastNBytes)
                throw new ArgumentOutOfRangeException(nameof(lastNBytes));

            var content = new byte[lastNBytes];

            var offset = (int)(fileStream.Length - lastNBytes);

            fileStream.Seek(offset, SeekOrigin.Begin);
            await fileStream.ReadAsync(content, 0, lastNBytes);

            return content;
        }

        public async Task<byte[]> ReadAsync(int firstNBytes, int offset)
        {
            if (fileStream.Length < firstNBytes)
                throw new ArgumentOutOfRangeException(nameof(firstNBytes));

            var content = new byte[firstNBytes];

            fileStream.Seek(offset, SeekOrigin.Begin);
            await fileStream.ReadAsync(content, 0, firstNBytes);

            return content;
        }

        public async Task<byte[]> ReadNextAsync(int nBytes)
        {
            if (fileStream.Length < nBytes + fileStream.Position)
                throw new ArgumentOutOfRangeException(nameof(nBytes));

            var content = new byte[nBytes];

            await fileStream.ReadAsync(content, 0, nBytes);

            return content;
        }

        public void Remove(int lastNBytes)
        {
            if (fileStream.Length < lastNBytes)
                throw new ArgumentOutOfRangeException(nameof(lastNBytes));

            fileStream.SetLength(fileStream.Length - lastNBytes);
        }

        public void Dispose()
        {
            if (fileStream != null)
            {
                fileStream.Dispose();
                fileStream.Close();
                fileStream = null;
            }
        }
    }
}
