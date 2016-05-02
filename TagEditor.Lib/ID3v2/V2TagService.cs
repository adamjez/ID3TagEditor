using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TagEditor.Lib.Common;
using TagEditor.Lib.ID3v1;
using TagEditor.Lib.Interfaces;

namespace TagEditor.Lib.ID3v2
{
    class V2TagService : TagService
    {
        private const string tag = "ID3";

        public V2TagService(IFile file) 
            : base(file)
        {
        }

        public override async Task<bool> ValidFormatAsync()
        {
            var tagBytes = await File.ReadAsync(10, 0);

            var tagString = Encoding.ASCII.GetString(tagBytes.Take(3).ToArray());

            return tagString == tag;
        }

        public override Task<ITagInformation> ParseAsync()
        {
            throw new NotImplementedException();
        }

        public override Task SaveAsync(ITagInformation tags)
        {
            throw new NotImplementedException();
        }

        public override Task RemoveTags()
        {
            throw new NotImplementedException();
        }
    }
}
