using System;
using System.Text;
using TagEditor.Library.Interfaces;

namespace TagEditor.Library.ID3v1
{
    public abstract class BasicTag<T> : ITag<T>, ITagValidation<T>
    {
        protected readonly int BytesCapacity;
        protected BasicTag(int bytesCapacity)
        {
            BytesCapacity = bytesCapacity;
        }

        public T Content { get; protected set; }
        public virtual byte[] Render(int capacity = -1)
        {
            if (capacity < 0)
                capacity = BytesCapacity;

            var array = Encoding.GetEncoding(1252)
                .GetBytes(Content.ToString());

            if(capacity > 0)
                Array.Resize(ref array, capacity);

            return array;
        }

        public override string ToString()
        {
            return Content.ToString();
        }

        public virtual void SetValue(T value)
        {
            if (!Validate(value))
                throw new ArgumentException(nameof(value));

            Content = value;
        }

        public abstract void Parse(byte[] content);

        public abstract bool Validate(T val);
    }
}