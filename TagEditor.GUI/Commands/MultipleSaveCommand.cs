using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;
using TagEditor.Core.Common;
using TagEditor.GUI.Models;
using TagEditor.GUI.ViewModels;
using TagLib;
using PictureType = TagEditor.Core.ID3v2.Frame.Types.PictureType;

namespace TagEditor.GUI.Commands
{
    public class MultipleSaveCommand : BaseCommand<DetailViewModel>
    {
        public MultipleSaveCommand(DetailViewModel viewModel)
            : base(viewModel)
        {
        }

        public override bool CanExecute(object parameter) => ViewModel.MoreFiles;

        public override async void Execute(object parameter)
        {
            ViewModel.IsBusy = true;

            var editor = new Core.Common.TagEditor();
            foreach (var path in ViewModel.Paths)
            {
                var file = await StorageFile.GetFileFromPathAsync(path);
                using (var audioFile = new AudioFile(await file.OpenStreamForWriteAsync()))
                {
                    var info = await editor.RetrieveTagsAsync(audioFile, TagType.ID3v2)
                        ?? new TagInformation();

                    info.Album.SetValue(Set(info.Album.Content, ViewModel.Tag.Album));
                    info.Artist.SetValue(Set(info.Artist.Content, ViewModel.Tag.Artist));
                    info.Year.SetValue((int?)Set((uint?)info.Year.Content, ViewModel.Tag.Year));

                    if (ViewModel.Tag.Genre.IsEdited && !string.IsNullOrEmpty(ViewModel.Tag.Genre.Content))
                    {
                        info.Genre.Type = ViewModel.Tag.Genre.Content;
                    }

                    if (ViewModel.Tag.AlbumArt.Content != null)
                    {
                        info.AlbumArt.SetValue(ViewModel.Tag.AlbumArt.Content.Content);
                        info.AlbumArt.MimeType = ViewModel.Tag.AlbumArt.Content.MimeType;
                        info.AlbumArt.Type = PictureType.FrontCover;
                    }

                    await editor.SetTags(audioFile, info, TagType.ID3v2);
                    await editor.SetTags(audioFile, info, TagType.ID3v1);
                }
            }

            //ViewModel.FileInformations.Clear();
            //ViewModel.FileInformations.Add(await FileInformation.Load(file));

            ViewModel.IsBusy = false;
        }

        private T Set<T>(T content, MultiInfo<T> info)
        {
            if (info.IsEdited)
            {
                return info.Content;
            }
            return content;
        }
    }
}
