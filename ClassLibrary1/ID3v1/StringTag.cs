using TagEditor.Core.Utility;

namespace TagEditor.Core.ID3v1
{
    public class StringBasicTag : BasicTag<string>
    {
        private readonly int _maxLength;
        private readonly bool isNotLimited;

        public StringBasicTag(int maxLength = -1)
            : base(maxLength)
        {
            Content = string.Empty;

            if (maxLength > 0)
            {
                _maxLength = maxLength;
            }
            else
            {
                isNotLimited = true;
            }
        }

        public override void Parse(byte[] content)
        {
            SetValue(Extensions.GetDefaultEncodingV1().GetString(content));
        }

        public override bool Validate(string val)
        {
            return isNotLimited || val.Length <= _maxLength;
        }
    }
}
