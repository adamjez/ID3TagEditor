using System;
using System.Threading.Tasks;
using Windows.Storage;
using TagEditor.GUI.ViewModels;

namespace TagEditor.GUI.Models
{
    public class FileInformation : NotificationBase
    {
        public static async Task<FileInformation> Load(StorageFile file)
        {
            var properties = await file.GetBasicPropertiesAsync();
            var result = new FileInformation
            {
                Format = file.DisplayType,
                ModifiedAt = properties.DateModified,
                Size = properties.Size / 1000,
                Path = file.Path
            };

            return result;
        }

        public ulong Size { get; set; }
        public string Format { get; set; }
        public string Path { get; set; }
        public DateTimeOffset ModifiedAt { get; set; }
    }
}