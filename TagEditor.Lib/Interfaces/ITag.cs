namespace TagEditor.Lib.Interfaces
{
    public interface ITag<T>
    {
        void SetValue(T value);
        T Content { get; }
    }
}