using System;
using System.Linq;
using TagEditor.Library.Utility;

namespace TagEditor.Library.ID3v1
{
    public class NumberTag : BasicTag<uint?>
    {
        public NumberTag(int numberOfBytes)
            : base(numberOfBytes)
        {   }

        public override void Parse(byte[] content)
        {
            SetValue(content.Length == 0
                ? null
                : (uint?)content.ToUInt());
        }

        public override bool Validate(uint? val)
        {
            // We need to compute maximal number we can store
            return !val.HasValue || (val > 0 && val <= Math.Pow(val.Value, BytesCapacity * 8));
        }

        public override byte[] Render(int capacity = -1)
        {
            var bytes = Content.HasValue 
                ? BitConverter.GetBytes(Content.Value) : new byte[BytesCapacity];

            return bytes.Take(BytesCapacity).ToArray();
        }
    }
}