﻿using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TagEditor.Core.Common;
using TagEditor.Core.Interfaces;
using TagEditor.Core.Utility;

namespace TagEditor.Core.ID3v1
{
    public class V1TagService : TagService
    {
        private const int tagSize = 128;
        private const string tag = "TAG";

        public V1TagService(IFile file)
            : base(file)
        {   }

        public override async Task<bool> ParseHeaderAsync()
        {
            await LoadData();

            if (Content == null)
                return false;

            var newArray = Content.Take(3).ToArray();

            var result = Encoding.ASCII.GetString(newArray);

            return result == tag;
        }

        public override async Task<ITagInformation> ParseAsync()
        {
            if(!await ParseHeaderAsync())
                throw new InvalidOperationException("File doesn't have valid ID3v1 tag presented");

            var info = new TagInformation();

            using (var ms = new MemoryStream(Content))
            {
                info.Title.Parse((await ms.ReadBytesAsync(30, 3)).ToClearArray());
                info.Artist.Parse((await ms.ReadBytesAsync(30)).ToClearArray());
                info.Album.Parse((await ms.ReadBytesAsync(30)).ToClearArray());
                info.Year.Parse((await ms.ReadBytesAsync(4)).ToClearArray());

                var commentArray = (await ms.ReadBytesAsync(30)).ToArray();
                info.Comment.Parse(commentArray.ToClearArray());

                if (commentArray[28] == 0)
                {
                    // ID3v1.1 detected => track number is presented
                    info.TrackNumber.Parse(commentArray.Skip(29).ToClearArray());
                }
                info.Genre.Parse((await ms.ReadBytesAsync(1)).ToArray());
            }
           

            return info;
        }

        public override async Task SaveAsync(ITagInformation tags)
        {
            byte[] buffer = new byte[tagSize];

            using (var ms = new MemoryStream(buffer))
            {
                await ms.WriteBytesAsync(Encoding.ASCII.GetBytes(tag));
                await ms.WriteBytesAsync(tags.Title.Render(30));
                await ms.WriteBytesAsync(tags.Artist.Render(30));
                await ms.WriteBytesAsync(tags.Album.Render(30));
                await ms.WriteBytesAsync(tags.Year.Render());
                await ms.WriteBytesAsync(tags.Comment.Render(30).Take(28).ToArray());
                await ms.WriteBytesAsync(new byte[] { 0 });
                await ms.WriteBytesAsync(tags.TrackNumber.Render());
                await ms.WriteBytesAsync(tags.Genre.Render());
            }

            var offset = 0;
            // Overwrite existing tags if exists
            if (await ParseHeaderAsync())
                offset = -buffer.Length;

            await File.WriteAsync(buffer, offset, true);
        }

        public override async Task RemoveTags()
        {
            if (!await ParseHeaderAsync())
            {
                Debug.WriteLine("File doesn't have valid ID3v1 tag presented");
                return;
            }

            File.Remove(tagSize);
        }

        private async Task LoadData()
        {
            if (Content == null)
            {
                Content = await File.ReadAsync(tagSize);
            }
        }
    }
}
