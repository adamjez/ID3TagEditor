using System;
using System.Diagnostics;
using System.Threading.Tasks;
using TagEditor.Core.ID3v1;
using TagEditor.Core.ID3v2;
using TagEditor.Core.Interfaces;

namespace TagEditor.Core.Common
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

        public async Task<ITagInformation> RetrieveBasicTagsAsync(IFile file)
        {
            TagService service = new V1TagService(file);

            if (!await service.ParseHeaderAsync())
            {
                service = new V2TagService(file);
            }
            try
            {
                return await service.ParseAsync();
            }
            catch (Exception exc)
            {
                Debug.WriteLine("Exception While retrieving basi tags: " + exc.Message);
                return null;
            }
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
