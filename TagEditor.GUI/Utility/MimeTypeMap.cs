using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TagEditor.GUI.Utility
{
    public class MimeTypeMap
    {
        private static IDictionary<string, string> _mappings = new Dictionary<string, string>() {
        {".art", "image/x-jg"},
        {".bmp", "image/bmp"},
        {".cmx", "image/x-cmx"},
        {".cod", "image/cis-cod"},
        {".dib", "image/bmp"},
        {".gif", "image/gif"},
        {".ico", "image/x-icon"},
        {".ief", "image/ief"},
        {".jfif", "image/pjpeg"},
        {".jpe", "image/jpeg"},
        {".jpeg", "image/jpeg"},
        {".jpg", "image/jpeg"},
        {".mac", "image/x-macpaint"},
        {".pbm", "image/x-portable-bitmap"},
        {".pct", "image/pict"},
        {".pgm", "image/x-portable-graymap"},
        {".pic", "image/pict"},
        {".pict", "image/pict"},
        {".png", "image/png"},
        {".pnm", "image/x-portable-anymap"},
        {".pnt", "image/x-macpaint"},
        {".pntg", "image/x-macpaint"},
        {".pnz", "image/png"},
        {".ppm", "image/x-portable-pixmap"},
        {".qti", "image/x-quicktime"},
        {".qtif", "image/x-quicktime"},
        {".ras", "image/x-cmu-raster"},
        {".rf", "image/vnd.rn-realflash"},
        {".rgb", "image/x-rgb"},
        {".tif", "image/tiff"},
        {".tiff", "image/tiff"},
        {".wbmp", "image/vnd.wap.wbmp"},
        {".wdp", "image/vnd.ms-photo"},
        {".xbm", "image/x-xbitmap"},
        {".xpm", "image/x-xpixmap"},
        {".xwd", "image/x-xwindowdump"},
        };

        public static string GetMimeType(string extension)
        {
            if (extension == null)
            {
                throw new ArgumentNullException(nameof(extension));
            }

            if (!extension.StartsWith("."))
            {
                extension = "." + extension;
            }

            string mime;

            return _mappings.TryGetValue(extension, out mime) ? mime : "application/octet-stream";
        }
    }
}
