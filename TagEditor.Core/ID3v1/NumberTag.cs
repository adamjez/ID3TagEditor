using System;
using System.Linq;
using TagEditor.Core.Utility;

namespace TagEditor.Core.ID3v1
{
    public class NumberTag : BasicTag<uint>
    {
        public NumberTag(int numberOfBytes)
            : base(numberOfBytes)
        {   }

        public uint TrackCount { get; set; }

        public override void Parse(byte[] content)
        {
            SetValue(content.Length == 0 ? 0 : content.ToUInt());
        }

        public override bool Validate(uint val)
        {
            // We need to compute maximal number we can store
            return val <= Math.Pow(val, BytesCapacity * 8);
        }

        public override byte[] Render(int capacity = -1)
        {
            var bytes = Content != 0 
                ? BitConverter.GetBytes(Content) : new byte[BytesCapacity];

            return bytes.Take(BytesCapacity).ToArray();
        }
    }
}