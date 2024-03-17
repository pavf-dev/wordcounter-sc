namespace FileWordCounter;

public class FileWordCounter
{
    private readonly IWordCounter _wordCounter;
    
    public FileWordCounter(IWordCounterFabric wordCounterFabric)
    {
        _wordCounter = wordCounterFabric.Create();
    }
    
    public async Task<IReadOnlyDictionary<string, int>> CountWords(string filePath)
    {
        if (!File.Exists(filePath))
        {
            throw new ArgumentException($"File {filePath} does not exist");
        }
        
        var text = await File.ReadAllTextAsync(filePath);
        
        return _wordCounter.CountWords(text, Separators.DefaultSeparators);
    }
}