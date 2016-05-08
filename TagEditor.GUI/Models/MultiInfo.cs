using System.Collections.Generic;
using System.Collections.ObjectModel;
using TagEditor.GUI.ViewModels;

namespace TagEditor.GUI.Models
{
    public class MultiInfo<T> : NotificationBase
    {
        private bool multipleContent;
        private T content;
        private ObservableCollection<T> sourceItems;
        private bool isEdited;

        public MultiInfo(T item)
        {
            MultipleContent = false;
            Content = item;
            IsEdited = true;
        }

        public MultiInfo(IEnumerable<T> items)
        {
            MultipleContent = true;
            SourceItems = new ObservableCollection<T>(items);
        }

        public bool MultipleContent
        {
            get { return multipleContent; }
            set { SetProperty(ref multipleContent, value); }
        }

        public bool IsEdited
        {
            get { return isEdited; }
            set { SetProperty(ref isEdited, value); }
        }

        public T Content
        {
            get { return content; }
            set { SetProperty(ref content, value); }
        }

        public ObservableCollection<T> SourceItems
        {
            get { return sourceItems; }
            private set { SetProperty(ref sourceItems, value); }
        }
    }
}
