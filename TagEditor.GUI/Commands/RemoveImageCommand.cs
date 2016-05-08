using TagEditor.GUI.Models;
using TagEditor.GUI.ViewModels;

namespace TagEditor.GUI.Commands
{
    public class RemoveImageCommand : BaseCommand<DetailViewModel>
    {
        public RemoveImageCommand(DetailViewModel viewModel) : base(viewModel)
        {
        }

        public override bool CanExecute(object parameter) => true;

        public override void Execute(object parameter)
        {
            ViewModel.Tag.AlbumArt.Content = new ImageTag();
        }
    }
}