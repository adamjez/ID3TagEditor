﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TagEditor.Lib.Utility
{
    public static class Extensions
    {
        public static byte[] ToClearArray(this IEnumerable<byte> content)
        {
            return content.TakeWhile(ch => ch != 0).ToArray();
        }

        public static uint ToUInt(this byte[] content)
        {
            uint result = 0;
            foreach (byte number in content)
            {
                result <<= 8;
                result += number;
            }
            return result;
        }

        public static async Task WriteBytesAsync(this Stream stream, byte[] content)
        {
            await stream.WriteAsync(content, 0, content.Length);
        }

        public static async Task<byte[]> ReadBytesAsync(this Stream stream, int size, int skip = 0)
        {
            var result = new byte[size];

            stream.Seek(skip, SeekOrigin.Current);
            var ret = await stream.ReadAsync(result, 0, size);

            if(ret != size)
                throw new ArgumentOutOfRangeException(nameof(size));

            return result;
        }
    }
}