using System;
using System.IO;
using System.Threading.Tasks;
using TagEditor.Lib.Interfaces;

namespace TagEditor.Lib.Common
{
    public class AudioFile : IFile, IDisposable
    {
        private FileStream _fileStream;

        public void Open(string path, bool readOnly = true)
        {
            var accessLevel = readOnly ? FileAccess.Read : FileAccess.ReadWrite;
            _fileStream = File.Open(path, FileMode.Open, accessLevel);
        }

        public async Task WriteAsync(byte[] content, int offset, bool reverseDirection = false)
        {
            if(!_fileStream.CanWrite)
                throw new InvalidOperationException("Cannot write into file opened for only reading");

            if (reverseDirection)
                offset = (int) (_fileStream.Length - offset);

            _fileStream.Seek(offset, SeekOrigin.Begin);
            await _fileStream.WriteAsync(content, 0, content.Length);
        }

        public async Task<byte[]> ReadAsync(int lastNBytes)
        {
            if (_fileStream.Length < lastNBytes)
                throw new ArgumentOutOfRangeException(nameof(lastNBytes));

            var content = new byte[lastNBytes];

            var offset = (int)(_fileStream.Length - lastNBytes);

            _fileStream.Seek(offset, SeekOrigin.Begin);
            await _fileStream.ReadAsync(content, 0, lastNBytes);

            return content;
        }

        public async Task<byte[]> ReadAsync(int firstNBytes, int offset)
        {
            if (_fileStream.Length < firstNBytes)
                throw new ArgumentOutOfRangeException(nameof(firstNBytes));

            var content = new byte[firstNBytes];

            _fileStream.Seek(offset, SeekOrigin.Begin);
            await _fileStream.ReadAsync(content, 0, firstNBytes);

            return content;
        }

        public void Remove(int lastNBytes)
        {
            if (_fileStream.Length < lastNBytes)
                throw new ArgumentOutOfRangeException(nameof(lastNBytes));

            _fileStream.SetLength(_fileStream.Length - lastNBytes);
        }

        public void Dispose()
        {
            if (_fileStream != null)
            {
                _fileStream.Dispose();
                _fileStream.Close();
                _fileStream = null;
            }
        }
    }
}
