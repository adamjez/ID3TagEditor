namespace TagEditor.Library.Interfaces
{
    public interface ITagValidation<T>
    {
        bool Validate(T val);
    }
}
