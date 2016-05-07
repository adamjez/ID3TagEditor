using System.Threading.Tasks;
using TagEditor.Library.Common;

namespace TagEditor.Library.Interfaces
{
    public interface ITagEditor
    {
        Task<ITagInformation> RetrieveTagsAsync(IFile file, TagType type);

        Task SetTags(IFile file, ITagInformation tags, TagType type);
    }
}