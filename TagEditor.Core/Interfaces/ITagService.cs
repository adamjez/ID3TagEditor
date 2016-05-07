using System.Threading.Tasks;
using TagEditor.Core.Common;

namespace TagEditor.Core.Interfaces
{
    public interface ITagService
    {
        Task<bool> ParseHeaderAsync();

        Task<ITagInformation> ParseAsync();

        Task SaveAsync(ITagInformation tags);

        Task RemoveTags();
    }
}
