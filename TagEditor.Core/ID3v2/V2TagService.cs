using System;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using TagEditor.Core.Common;
using TagEditor.Core.ID3v2.Frame;
using TagEditor.Core.Interfaces;
using TagEditor.Core.Utility;

namespace TagEditor.Core.ID3v2
{
    class V2TagService : TagService
    {
        private const int supportedMajorVersion = 3;

        private Header header;
        private ExtendedHeader extendedHeader;

        private uint currentPosition = 0;
        public V2TagService(IFile file)
            : base(file)
        {
        }

        /// <summary>
        /// Source http://id3.org/id3v2.3.0#ID3v2_header
        /// </summary>
        /// <returns>Returns True if library can parse ID3v2 tag in file</returns>
        public override async Task<bool> ParseHeaderAsync()
        {
            var tagBytes = await File.ReadAsync(10, 0);

            header = await Header.Parse(tagBytes);

            return header != null && header.MajorVersion == supportedMajorVersion;
        }

        public override async Task<ITagInformation> ParseAsync()
        {
            if (header == null && !await ParseHeaderAsync())
                throw new InvalidOperationException("File doesn't have valid ID3v2 tag presented");

            Debug.Assert(header != null, "Header != null");

            // Parse extended header if exists
            if (FlagsHelper.IsSet(header.Flags, HeaderFlags.Extended))
            {
                var extendedBytes = await File.ReadNextAsync(10);

                extendedHeader = await ExtendedHeader.Parse(extendedBytes);

                // TODO: do something with extended header content
                await File.ReadNextAsync(extendedHeader.Size);
                currentPosition = 10 + extendedHeader.Size;
            }

            var information = new TagInformation();
            while (currentPosition + 10 < header.Size)
            {
                try
                {
                    var frame = await Frame.Frame.Parse(File);
                    currentPosition += frame.Header.FullSize;

                    FrameTagMaping.Fill(frame.Base, information);
                }
                catch (EndOfFramesException)
                {
                    break;
                }

            }

            return information;
        }

        public override async Task SaveAsync(ITagInformation tags)
        {
            var replace = 0;
            // Overwrite existing tags if exists
            if (await ParseHeaderAsync())
            {
                replace = (int)(header.Size + 10);
            }

            var frames = FrameTagMaping.CreateFrames(tags)
                .Select(baseFrame => new Frame.Frame(baseFrame).Render())
                .Combine();

            var headerBytes = new Header
            {
                Size = (uint)frames.Length,
                MajorVersion = supportedMajorVersion
            }.Render();

            var buffer = new byte[10 + frames.Length];
            Buffer.BlockCopy(headerBytes, 0, buffer, 0, 10);
            Buffer.BlockCopy(frames, 0, buffer, 10, frames.Length);

            await File.WriteAtBeginningAsync(buffer, replace);
        }

        public override Task RemoveTags()
        {
            throw new NotImplementedException();
        }
    }
}
