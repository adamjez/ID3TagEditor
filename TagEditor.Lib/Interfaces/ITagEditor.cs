using System.Threading.Tasks;
using TagEditor.Lib.Common;
using TagEditor.Lib.ID3v1;

namespace TagEditor.Lib.Interfaces
{
    public interface ITagEditor
    {
        Task<ITagInformation> RetrieveTagsAsync(IFile file, TagType type);

        Task SetTags(IFile file, ITagInformation tags, TagType type);
    }
}