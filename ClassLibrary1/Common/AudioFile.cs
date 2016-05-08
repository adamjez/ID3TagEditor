using System;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;
using TagEditor.Core.Interfaces;

namespace TagEditor.Core.Common
{
    public class AudioFile : IFile, IDisposable
    {
        private Stream fileStream;
        private static int bufferSize = 65536;

        public AudioFile(Stream filestream, bool readOnly = false)
        {
            var accessLevel = readOnly ? FileAccess.Read : FileAccess.ReadWrite;

            this.fileStream = filestream;
        }

        public async Task WriteAsync(byte[] content, int offset, bool reverseDirection = false)
        {
            if (reverseDirection)
                offset = (int)(fileStream.Length - offset);

            fileStream.Seek(offset, SeekOrigin.Begin);
            await WriteAsync(content);
            fileStream.SetLength(offset + content.Length);
        }

        public async Task WriteAtBeginningAsync(byte[] content, int replaceLength)
        {
            fileStream.Seek(0, SeekOrigin.Begin);

            var resultLength = content.Length + fileStream.Length - replaceLength;
            if (content.Length == replaceLength)
            {
                await WriteAsync(content);
            }
            else if (content.Length <= replaceLength)
            {
                // Can write there tag 
                await WriteAsync(content);
                // Shift data to beginning
                await RemoveBlock(content.Length, replaceLength - content.Length);
            }
            else
            {
                fileStream.SetLength(resultLength);

                // First make space for tag
                var differenceLength = content.Length - replaceLength;
                var readPosition = fileStream.Length - bufferSize;
                var writePosition = readPosition + differenceLength;
                var canRead = true;
                while (canRead)
                {
                    var remains = readPosition > replaceLength
                       ? bufferSize
                       : bufferSize - (replaceLength - readPosition);
                    if (remains == 0)
                        break;
                    if (remains < bufferSize)
                        canRead = false;
                    // Correct position
                    readPosition += bufferSize - remains;
                    fileStream.Position = readPosition;

                    var buffer = await ReadNextAsync((int)remains);
                    readPosition -= buffer.Length;

                    writePosition += bufferSize - remains;
                    fileStream.Position = writePosition;
                    await WriteAsync(buffer);
                    writePosition -= buffer.Length;
                }

                fileStream.Seek(0, SeekOrigin.Begin);
                await WriteAsync(content);
            }
        }

        public async Task WriteAsync(byte[] content)
        {
            if (!fileStream.CanWrite)
                throw new InvalidOperationException("Cannot write into file opened for only reading");

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

        public async Task<byte[]> ReadNextAsync(int length)
        {
            if (length < 0)
                throw new ArgumentOutOfRangeException(nameof(length));

            if (length == 0)
                return new byte[0];

            var content = new byte[length];

            int count = 0, read = 0, needed = length;
            do
            {
                count = await fileStream.ReadAsync(content, read, needed);

                read += count;
                needed -= count;
            } while (needed > 0 && count != 0);

            return content;
        }

        public void Remove(int lastNBytes)
        {
            if (fileStream.Length < lastNBytes)
                throw new ArgumentOutOfRangeException(nameof(lastNBytes));

            fileStream.SetLength(fileStream.Length - lastNBytes);
        }

        public async Task RemoveBlock(long start, long length)
        {
            if (length <= 0)
                return;

            var readPosition = start + length;
            var writePosition = start;

            var buffer = new byte[] {1};
            var iterations = 0;
            var shiftLength = fileStream.Length - start;
            while (shiftLength > 0)
            {
                fileStream.Position = readPosition;
                buffer = Test((int)Math.Min(shiftLength, bufferSize));
                shiftLength -= buffer.Length;
                readPosition += buffer.Length;

                fileStream.Position = writePosition;
                fileStream.Write(buffer, 0, buffer.Length);
                writePosition += buffer.Length;

                iterations++;
            }
            Debug.WriteLine("Iterations: " + iterations);


            fileStream.SetLength(writePosition);
        }

        public byte[] Test(int length)
        {
            if (length < 0)
                throw new ArgumentOutOfRangeException(nameof(length));

            if (length == 0)
                return new byte[0];

            var content = new byte[length];

            int count = 0, read = 0, needed = length;
            do
            {
                count = fileStream.Read(content, read, needed);

                read += count;
                needed -= count;
            } while (needed > 0 && count != 0);

            return content;
        }

        public void Dispose()
        {
            if (fileStream != null)
            {
                fileStream.Dispose();
                fileStream = null;
            }
        }
    }
}
