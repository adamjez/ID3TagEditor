namespace TagEditor.Library.Interfaces
{
    public interface ITag<T>
    {
        void SetValue(T value);
        T Content { get; }
    }
}