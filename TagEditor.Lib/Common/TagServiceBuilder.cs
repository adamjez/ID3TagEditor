using System;
using TagEditor.Lib.ID3v1;
using TagEditor.Lib.ID3v2;
using TagEditor.Lib.Interfaces;

namespace TagEditor.Lib.Common
{
    public static class TagServiceBuilder
    {
        public static ITagService ResolveService(IFile file, TagType type)
        {
            if(type == TagType.ID3v1)
                return new V1TagService(file);

            if(type == TagType.ID3v2)
                return new V2TagService(file);

            throw new ArgumentException("You have to choose between ID3v1 and ID3v2", nameof(type));
        }
    }
}