using System;
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
                result |= number;
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

            if (ret != size)
                throw new ArgumentOutOfRangeException(nameof(size));

            return result;
        }

        public static async Task<byte> ReadByteAsync(this Stream stream)
        {
            var result = await ReadBytesAsync(stream, 1);

            return result[0];
        }

        public static T[] SubArray<T>(this T[] data, int index, int length)
        {
            T[] result = new T[length];
            Array.Copy(data, index, result, 0, length);
            return result;
        }

        public static T[] SubArray<T>(this T[] data, int index)
        {
            var length = data.Length - index;
            return data.SubArray(index, length);
        }

        public static T ToEnum<T>(this string value)
        {
            return (T)Enum.Parse(typeof(T), value, true);
        }

        public static byte[] GetDelimiter(this Encoding encoding)
        {
            if (encoding.WindowsCodePage == 1252)
            {
                return new byte[] { 0x00 };
            }
            else
            {
                return new byte[] { 0x00, 0x00 };
            }
        }

        public static byte GetByte(this Encoding encoding)
        {
            if (encoding.WindowsCodePage == 1252)
            {
                return 0x00;
            }
            else
            {
                return 0x01;
            }
        }


        // Source: http://stackoverflow.com/questions/283456/byte-array-pattern-search
        public static int IndexOf(this byte[] self, byte[] candidate)
        {
            if (IsEmptyLocate(self, candidate))
                return -1;

            for (int i = 0; i < self.Length; i++)
            {
                if (!IsMatch(self, i, candidate))
                    continue;

                return i;
            }
            return -1;
        }

        static bool IsMatch(byte[] array, int position, byte[] candidate)
        {
            if (candidate.Length > (array.Length - position))
                return false;

            for (int i = 0; i < candidate.Length; i++)
                if (array[position + i] != candidate[i])
                    return false;

            return true;
        }

        static bool IsEmptyLocate(byte[] array, byte[] candidate)
        {
            return array == null
                || candidate == null
                || array.Length == 0
                || candidate.Length == 0
                || candidate.Length > array.Length;
        }

        public static byte[] Combine(this IEnumerable<byte[]> arrays)
        {
            var enumerable = arrays.ToArray();
            byte[] rv = new byte[enumerable.Sum(a => a.Length)];
            int offset = 0;
            foreach (byte[] array in enumerable)
            {
                System.Buffer.BlockCopy(array, 0, rv, offset, array.Length);
                offset += array.Length;
            }
            return rv;
        }
    }
}
