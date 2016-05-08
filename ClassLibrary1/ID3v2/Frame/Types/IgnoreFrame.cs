namespace TagEditor.Core.ID3v2.Frame.Types
{
    internal class IgnoreFrame : BaseFrame
    {
        public IgnoreFrame(FrameType type) : base(type)
        {
        }

        public override void Parse(byte[] bytes)
        {
            // ignore content of this frame
        }

        public override byte[] Render()
        {
            throw new System.NotImplementedException();
        }
    }
}