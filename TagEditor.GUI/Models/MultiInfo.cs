using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using TagEditor.GUI.ViewModels;

namespace TagEditor.GUI.Models
{
    public class MultiInfo<T> : NotificationBase
    {
        private T content;
        private ObservableCollection<T> sourceItems;
        private bool isEdited;
        private bool multipleContent;

        public MultiInfo(T item)
            : this()
        {
            Content = item;
            IsEdited = true;
        }

        public MultiInfo(IEnumerable<T> items)
        {
            SourceItems = new ObservableCollection<T>(items);
        }

        public MultiInfo(bool isEdited = false)
        {
            IsEdited = isEdited;
            SourceItems = new ObservableCollection<T>();
        }

        public bool MultipleContent
        {
            get { return multipleContent; }
            set { SetProperty(ref multipleContent, value); ; }
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

        public void AddUniqueToItems(T item, Func<T, T, Boolean> compare = null)
        {
            if (sourceItems.All(sourceItem => !(compare?.Invoke(item, sourceItem) ?? sourceItem.Equals(item))))
            {
                SourceItems.Add(item);
            }

            if (SourceItems.Count > 1)
            {
                MultipleContent = true;
            }
        }

        public void ClearItems()
        {
            SourceItems.Clear();
            MultipleContent = false;
        }

        public MultiInfo<T> Prepare()
        {
            if (sourceItems.Count() == 1)
            {
                Content = sourceItems.First();
                IsEdited = true;
            }

            return this;
        }
    }
}
