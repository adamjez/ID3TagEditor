namespace TagEditor.Core.Interfaces
{
    public interface ITagValidation<T>
    {
        bool Validate(T val);
    }
}
