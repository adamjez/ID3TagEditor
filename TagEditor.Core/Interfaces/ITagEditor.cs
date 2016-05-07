using System.Threading.Tasks;
using TagEditor.Core.Common;

namespace TagEditor.Core.Interfaces
{
    public interface ITagEditor
    {
        Task<ITagInformation> RetrieveTagsAsync(IFile file, TagType type);

        Task SetTags(IFile file, ITagInformation tags, TagType type);
    }
}