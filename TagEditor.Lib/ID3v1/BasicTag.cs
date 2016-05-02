using System;
using System.Text;
using TagEditor.Lib.Interfaces;

namespace TagEditor.Lib.ID3v1
{
    public abstract class BasicTag<T> : ITag<T>, ITagValidation<T>
    {
        protected readonly int BytesCapacity;
        protected BasicTag(int bytesCapacity)
        {
            BytesCapacity = bytesCapacity;
        }

        public T Content { get; protected set; }
        public virtual byte[] Render()
        {
            var array = Encoding.GetEncoding(1252)
                .GetBytes(Content.ToString());
            Array.Resize(ref array, BytesCapacity);

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