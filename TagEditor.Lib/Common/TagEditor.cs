using System;
using System.Threading.Tasks;
using TagEditor.Lib.Interfaces;

namespace TagEditor.Lib.Common
{
    public class TagEditor : ITagEditor
    {
        public async Task<ITagInformation> RetrieveTagsAsync(IFile file, TagType type)
        {
            var service = TagServiceBuilder.ResolveService(file, type);

            if (!await service.ParseHeaderAsync())
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

        public async Task RemoveTags(IFile file, TagType type)
        {
            var service = TagServiceBuilder.ResolveService(file, type);

            await service.RemoveTags();
        }
    }
}
