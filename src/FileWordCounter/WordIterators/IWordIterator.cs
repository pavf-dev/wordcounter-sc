namespace FileWordCounter.WordIterators;

public interface IWordIterator : IEnumerable<string>, IEnumerator<string>
{
    void Init(string text, char[] separators);
}