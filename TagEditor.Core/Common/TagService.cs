using System.Threading.Tasks;
using TagEditor.Core.Interfaces;

namespace TagEditor.Core.Common
{
    public abstract class TagService : ITagService
    {
        protected byte[] Content;
        protected IFile File;

        protected TagService(IFile file)
        {
            File = file;
        }

        public abstract Task<bool> ParseHeaderAsync();
        public abstract Task<ITagInformation> ParseAsync();
        public abstract Task SaveAsync(ITagInformation tags);
        public abstract Task RemoveTags();
    }
}