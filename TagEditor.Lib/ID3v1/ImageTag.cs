using System;
using System.Drawing;
using TagEditor.Lib.Interfaces;

namespace TagEditor.Lib.ID3v1
{
    public class ImageTag : ITag<Image>
    {
        public void SetValue(Image value)
        {
            Content = value;
        }

        public string Description { get; set; }

        public Image Content { get; private set;  }
    }
}