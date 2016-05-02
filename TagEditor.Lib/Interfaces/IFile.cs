﻿using System.IO;
using System.Threading.Tasks;

namespace TagEditor.Lib.Interfaces
{
    public interface IFile
    {
        void Open(string path, bool readOnly = true);

        Task WriteAsync(byte[] content, int offset, bool reverseDirection = false);

        Task<byte[]> ReadAsync(int lastNBytes);

        Task<byte[]> ReadAsync(int firstNBytes, int offset);

        Task<byte[]> ReadNextAsync(int nBytes);

        void Remove(int lastNBytes);

    }
}
