using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Media.Imaging;
using TagEditor.GUI.ViewModels;

namespace TagEditor.GUI.Models
{
    public abstract class GridItem : NotificationBase
    {
        private string title;
        private string description1;
        private string description2;
        private BitmapImage thumbnail;

        public string Title
        {
            get { return title; }
            set { SetProperty(ref title, value); }
        }

        public string Description1
        {
            get { return description1; }
            set { SetProperty(ref description1, value); }
        }

        public string Description2
        {
            get { return description2; }
            set { SetProperty(ref description2, value); }
        }

        public BitmapImage Thumbnail
        {
            get { return thumbnail; }
            set { SetProperty(ref thumbnail, value); }
        }

        public abstract Task LoadContent();
    }
}
