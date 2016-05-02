using System.Diagnostics;
using System.Threading.Tasks;
using TagEditor.Lib.Interfaces;
using TagEditor.Lib.Utility;

namespace TagEditor.Lib.ID3v2
{
    internal class Frame
    {
        public FrameHeader Header { get; set; }

        public static async Task<Frame> Parse(IFile file)
        {
            var frame = new Frame();
            frame.Header = await FrameHeader.Parse(await file.ReadNextAsync(10));

            if (FlagsHelper.IsSet(frame.Header.Flags2, FrameHeaderFlags2.Compression))
            {
                // Todo: process compression
                await file.ReadNextAsync(4);
                Debug.WriteLine("Frame header flag is set (FrameHeaderFlags2.Compression)");
            }

            if (FlagsHelper.IsSet(frame.Header.Flags2, FrameHeaderFlags2.Encryption))
            {
                // Todo: process encryption
                await file.ReadNextAsync(1);
                Debug.WriteLine("Frame header flag is set (FrameHeaderFlags2.Encryption)");
            }

            if (FlagsHelper.IsSet(frame.Header.Flags2, FrameHeaderFlags2.GroupingIdentity))
            {
                // Todo: process grouping identity
                await file.ReadNextAsync(1);
                Debug.WriteLine("Frame header flag is set (FrameHeaderFlags2.GroupingIdentity)");
            }


            return null;
        }
    }
}