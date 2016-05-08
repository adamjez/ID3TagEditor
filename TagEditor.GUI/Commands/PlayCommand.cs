using System;
using System.Linq;
using Windows.Media.Playback;
using Windows.Storage;
using TagEditor.GUI.ViewModels;

namespace TagEditor.GUI.Commands
{

    public class PlayCommand : BaseCommand<DetailViewModel>
    {
        public PlayCommand(DetailViewModel viewModel)
            : base(viewModel)
        {
        }

        public override bool CanExecute(object parameter) => !ViewModel.MoreFiles;

        public override async void Execute(object parameter)
        {
            if (BackgroundMediaPlayer.Current.CurrentState == MediaPlayerState.Playing)
            {
                BackgroundMediaPlayer.Current.Pause();
            }
            else
            {
                var file = await StorageFile.GetFileFromPathAsync(ViewModel.Paths.First());
                BackgroundMediaPlayer.Current.SetFileSource(file);
            }
        }
    }

}
