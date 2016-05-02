using System;
using System.Threading.Tasks;
using TagEditor.Lib.ID3v1;
using TagEditor.Lib.Interfaces;

namespace TagEditor.Lib.Common
{
    public abstract class TagService : ITagService
    {
        protected byte[] Content;
        protected IFile File;

        protected TagService(IFile file)
        {
            File = file;
        }

        public abstract Task<bool> ValidFormatAsync();
        public abstract Task<ITagInformation> ParseAsync();
        public abstract Task SaveAsync(ITagInformation tags);
        public abstract Task RemoveTags();
    }
}