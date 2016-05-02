using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TagEditor.Lib.ID3v1;

namespace TagEditor.Lib.Interfaces
{
    public interface ITagService
    {
        Task<bool> ParseHeaderAsync();

        Task<ITagInformation> ParseAsync();

        Task SaveAsync(ITagInformation tags);

        Task RemoveTags();
    }
}
