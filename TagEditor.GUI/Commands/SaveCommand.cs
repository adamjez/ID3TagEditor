using System;
using System.IO;
using System.Linq;
using Windows.Storage;
using TagEditor.Core.Common;
using TagEditor.GUI.ViewModels;
using TagLib;

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
            if (file.FileType == ".mp3")
            {
                using (var fs = await file.OpenStreamForWriteAsync())
                {
                    using (var audioFile = new AudioFile(fs))
                    {
                        var editor = new Core.Common.TagEditor();

                        var information = ViewModel.Tag.ToTagInformation();

                        //await editor.SetTags(audioFile, information, TagType.ID3v2);
                        await editor.SetTags(audioFile, information, TagType.ID3v1);
                    }
                }
            }
            else
            {
                using (var fs = await file.OpenStreamForWriteAsync())
                {
                    var tagFile = TagLib.File.Create(new StreamFileAbstraction(file.Name, fs,fs));

                    ViewModel.Tag.ToTag(tagFile.Tag);

                    tagFile.Save();
                    tagFile.Dispose();
                }
            }
            ViewModel.IsBusy = false;
        }
    }
}
