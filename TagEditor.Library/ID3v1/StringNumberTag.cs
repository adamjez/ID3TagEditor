using System;
using System.Text;

namespace TagEditor.Library.ID3v1
{
    public class StringNumberTag : BasicTag<int?>
    {
        public StringNumberTag(int numberOfDigits)
            : base(numberOfDigits)
        {   }

        public override void Parse(byte[] content)
        {
            if(content.Length > BytesCapacity)
                throw new ArgumentException(nameof(content));

            var encodedValue = Encoding.ASCII.GetString(content);
            var parsedValue = string.IsNullOrEmpty(encodedValue)
                ? null : (int?)int.Parse(encodedValue);

            SetValue(parsedValue);
        }

        public override bool Validate(int? val)
        {
            return !val.HasValue || (val > 0 && val.ToString().Length <= BytesCapacity);
        }
    }
}