using FileWordCounter.WordCounterLogic;
using FileWordCounter.WordIterators;
using FileWordCounter.WordNormalizers;

namespace FileWordCounter.Factories;

public class WordCounterDefaultFactory : IWordCounterFactory
{
    public IWordCounter Create()
    {
        var wordIterator = new DefaultWordIterator();
        var wordNormalizer = new DefaultEnglishWordNormalizer();
        
        return new WordCounter(wordIterator, wordNormalizer);
    }
}