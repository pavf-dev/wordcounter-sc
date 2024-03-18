namespace FileWordCounter.FileWordCounters;

public interface IFileWordCounter
{
    Task<IReadOnlyDictionary<string, int>> CountWords(string filePath);
}