using System;
using System.Diagnostics;
using System.IO;
using Windows.Storage;
using Windows.Storage.Pickers;
using Windows.UI.Xaml.Media.Imaging;
using TagEditor.Core.Utility;
using TagEditor.GUI.Utility;
using TagEditor.GUI.ViewModels;

namespace TagEditor.GUI.Commands
{
    public class LoadImageCommand : BaseCommand<DetailViewModel>
    {
        public LoadImageCommand(DetailViewModel viewModel) : base(viewModel)
        {
        }

        public override bool CanExecute(object parameter) => true;

        public override async void Execute(object parameter)
        {
            FileOpenPicker openPicker = new FileOpenPicker
            {
                ViewMode = PickerViewMode.Thumbnail,
                SuggestedStartLocation = PickerLocationId.PicturesLibrary
            };
            openPicker.FileTypeFilter.Add(".jpg");
            openPicker.FileTypeFilter.Add(".bmp");
            openPicker.FileTypeFilter.Add(".png");

            StorageFile file = await openPicker.PickSingleFileAsync();

            if (file != null)
            {
                try
                {
                    using (var stream = await file.OpenStreamForReadAsync())
                    {
                        var content = await stream.ReadBytesAsync((int)stream.Length);
                        await ViewModel.Tag.SetNewImage(content, MimeTypeMap.GetMimeType(file.FileType));
                    }

                }
                catch (Exception exc)
                {
                    Debug.WriteLine("Couldn't open given file: " + exc.Message);
                    return;
                }

            }
        }
    }
}