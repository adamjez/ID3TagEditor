using System;
using System.Threading.Tasks;
using TagEditor.Lib.ID3v1;
using TagEditor.Lib.Interfaces;

namespace TagEditor.Lib.Common
{
    public class TagEditor : ITagEditor
    {
        public async Task<ITagInformation> RetrieveTagsAsync(IFile file, TagType type)
        {
            var service = TagServiceBuilder.ResolveService(file, type);

            if (!await service.ValidFormatAsync())
            {
                throw new ArgumentException("File is in invalid format", nameof(file));
            }
            return await service.ParseAsync();
        }

        public async Task SetTags(IFile file, ITagInformation tags, TagType type)
        {
            var service = TagServiceBuilder.ResolveService(file, type);

            await service.SaveAsync(tags);
        }
    }
}
