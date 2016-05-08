using System;
using System.Diagnostics;
using System.Threading.Tasks;
using TagEditor.Core.ID3v2.Frame.Types;
using TagEditor.Core.Interfaces;
using TagEditor.Core.Utility;

namespace TagEditor.Core.ID3v2.Frame
{
    internal class Frame
    {
        public FrameHeader Header { get; set; }
        public BaseFrame Base { get; set; }

        public Frame()
        {   }

        public Frame(BaseFrame baseFrame)
        {
            Base = baseFrame;
            Header = new FrameHeader();
        }

        public static async Task<Frame> Parse(IFile file)
        {
            var frame = new Frame
            {
                Header = await FrameHeader.Parse(await file.ReadNextAsync(10))
            };

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

            try
            {
                frame.Base = FrameResolver.Resolve(frame.Header.FrameID.ToEnum<FrameType>());
            }
            catch (Exception)
            {
                Debug.WriteLine("Unknown frame type with FrameID: " + frame.Header.FrameID);
                frame.Base = new IgnoreFrame(FrameType.UNKNOWN);
            }
            frame.Base.Parse(await file.ReadNextAsync((int)frame.Header.Size));

            return frame;
        }

        public byte[] Render()
        {
            var frameBase = Base.Render();

            Header.Size = (uint)frameBase.Length;
            Header.FrameID = Base.Type.ToString();
            var header = Header.Render();

            var buffer = new byte[header.Length + frameBase.Length];
            Buffer.BlockCopy(header, 0, buffer, 0, header.Length);
            Buffer.BlockCopy(frameBase, 0, buffer, 10, frameBase.Length);

            return buffer;
        }
    }
}