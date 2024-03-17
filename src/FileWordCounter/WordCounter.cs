namespace FileWordCounter;

public interface IWordCounter
{
    IReadOnlyDictionary<string, int> CountWords(string text, char[] separators);
}

public class WordCounter : IWordCounter
{
    private readonly IWordIterator _wordIterator;
    private readonly IWordNormalizer _wordNormalizer;

    public WordCounter(
        IWordIterator wordIterator,
        IWordNormalizer wordNormalizer)
    {
        _wordIterator = wordIterator;
        _wordNormalizer = wordNormalizer;
    }
    
    public IReadOnlyDictionary<string, int> CountWords(string text, char[] separators)
    {
        if (string.IsNullOrWhiteSpace(text))
        {
            throw new ArgumentException($"Parameter {nameof(text)} can not be null or empty");
        }
        
        var initialCapacity = text.Length < 5 ? 1 : text.Length / 5 + 1;
        var result = new Dictionary<string, int>(initialCapacity);
        _wordIterator.Init(text, separators);
        
        foreach (var word in _wordIterator)
        {
            var normalizedWords = _wordNormalizer.Normalize(word);
            
            foreach (var normalizedWord in normalizedWords)
            {
                if (!result.TryAdd(normalizedWord, 1))
                {
                    result[normalizedWord]++;
                }
            }
        }
        
        return result;
    }
}