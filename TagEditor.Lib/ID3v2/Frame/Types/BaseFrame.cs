using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TagEditor.Lib.ID3v1;

namespace TagEditor.Lib.ID3v2.Frame.Types
{
    internal abstract class BaseFrame
    {
        public FrameType Type { get; set; }

        protected BaseFrame(FrameType type)
        {
            Type = type;
        }

        public abstract void Parse(byte[] bytes);
    }
}
