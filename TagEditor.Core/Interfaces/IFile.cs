using System.Threading.Tasks;

namespace TagEditor.Core.Interfaces
{
    public interface IFile
    {
        void Open(string path, bool readOnly = true);

        Task WriteAsync(byte[] content, int offset, bool reverseDirection = false);

        Task WriteAtBeginningAsync(byte[] content, int replaceLength);

        Task<byte[]> ReadAsync(int lastNBytes);

        Task<byte[]> ReadAsync(int firstNBytes, int offset);

        Task<byte[]> ReadNextAsync(uint nBytes);

        void Remove(int lastNBytes);

    }
}
