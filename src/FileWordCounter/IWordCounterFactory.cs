namespace FileWordCounter;

public interface IWordCounterFactory
{
    IWordCounter Create();
}

public class WordCounterFactory : IWordCounterFactory
{
    public IWordCounter Create()
    {
        var wordIterator = new DefaultWordIterator();
        var wordNormalizer = new DefaultEnglishWordNormalizer();
        
        return new WordCounter(wordIterator, wordNormalizer);
    }
}