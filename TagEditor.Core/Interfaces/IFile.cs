﻿using System.Threading.Tasks;

namespace TagEditor.Core.Interfaces
{
    public interface IFile
    {
        Task WriteAsync(byte[] content, int offset, bool reverseDirection = false);

        Task WriteAtBeginningAsync(byte[] content, int replaceLength);

        Task<byte[]> ReadAsync(int lastNBytes);

        Task<byte[]> ReadAsync(int firstNBytes, int offset);

        Task<byte[]> ReadNextAsync(int length);

        void Remove(int lastNBytes);

        Task RemoveBlock(long start, long length);
    }
}
