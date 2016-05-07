using System.Threading.Tasks;
using TagEditor.Library.Common;

namespace TagEditor.Library.Interfaces
{
    public interface ITagService
    {
        Task<bool> ParseHeaderAsync();

        Task<ITagInformation> ParseAsync();

        Task SaveAsync(ITagInformation tags);

        Task RemoveTags();
    }
}
