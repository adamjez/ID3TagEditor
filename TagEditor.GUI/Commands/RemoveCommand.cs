using System;
using System.IO;
using Windows.Storage;
using TagEditor.Core.Common;
using TagEditor.GUI.ViewModels;

namespace TagEditor.GUI.Commands
{
    public class RemoveCommand : BaseCommand<DetailViewModel>
    {
        public RemoveCommand(DetailViewModel viewModel)
            : base(viewModel)
        {
        }

        public override bool CanExecute(object parameter) => true;

        public override async void Execute(object parameter)
        {
            foreach (var path in ViewModel.Paths)
            {
                var file = await StorageFile.GetFileFromPathAsync(path);
                using (var fs = await file.OpenStreamForWriteAsync())
                {
                    using (var audioFile = new AudioFile(fs))
                    {
                        var editor = new Core.Common.TagEditor();

                        await editor.RemoveTags(audioFile, TagType.ID3v1);
                        await editor.RemoveTags(audioFile, TagType.ID3v2);
                    }
                }
                await ViewModel.LoadItem(ViewModel.Paths);
            }
        }
    }
}