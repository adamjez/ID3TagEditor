using System;
using System.IO;
using System.Linq;
using Windows.Storage;
using TagEditor.Core.Common;
using TagEditor.GUI.ViewModels;

namespace TagEditor.GUI.Commands
{

    public class SaveCommand : BaseCommand<DetailViewModel>
    {
        public SaveCommand(DetailViewModel viewModel)
            : base(viewModel)
        {
        }

        public override bool CanExecute(object parameter) => !ViewModel.MoreFiles;

        public override async void Execute(object parameter)
        {
            ViewModel.IsBusy = true;

            var file = await StorageFile.GetFileFromPathAsync(ViewModel.Paths.First());
            using (var stream = await file.OpenAsync(FileAccessMode.ReadWrite))
            {
                using (var audioFile = new AudioFile(stream.AsStream()))
                {
                    var editor = new Core.Common.TagEditor();
                    
                    var information = ViewModel.Tag.ToTagInformation();

                    await editor.SetTags(audioFile, information, TagType.ID3v2);
                    await editor.SetTags(audioFile, information, TagType.ID3v1);
                }
            }

            ViewModel.IsBusy = false;
        }
    }
}
