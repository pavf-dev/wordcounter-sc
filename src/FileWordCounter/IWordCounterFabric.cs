namespace FileWordCounter;

public interface IWordCounterFabric
{
    IWordCounter Create();
}

public class WordCounterFabric : IWordCounterFabric
{
    public IWordCounter Create()
    {
        var wordIterator = new DefaultWordIterator();
        var wordNormalizer = new DefaultEnglishWordNormalizer();
        
        return new WordCounter(wordIterator, wordNormalizer);
    }
}