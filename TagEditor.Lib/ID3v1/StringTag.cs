using System.Text;

namespace TagEditor.Lib.ID3v1
{
    public class StringBasicTag : BasicTag<string>
    {
        private readonly int _maxLength;

        public StringBasicTag(int maxLength)
            : base(maxLength)
        {
            Content = string.Empty;

            _maxLength = maxLength;
        }

        public override void Parse(byte[] content)
        {
            SetValue(Encoding.GetEncoding(1252).GetString(content));
        }

        public override bool Validate(string val)
        {
            return val.Length <= _maxLength;
        }
    }
}
