using FileWordCounter;
using FluentAssertions;

namespace FileWordCounterTests;

public class WordCounterTests
{
    [Test]
    public void Returns_expected_words_and_their_counters()
    {
        var wordCounter = new WordCounter(new DefaultWordIterator(), new DefaultEnglishWordNormalizer());

        var result = wordCounter.CountWords("This is a test text.\nHere is a question: Is it any good test(text)?", Separators.DefaultSeparators);
        
        result.Should().HaveCount(10);
        result.Should().Contain("this", 1);
        result.Should().Contain("is", 3);
        result.Should().Contain("a", 2);
        result.Should().Contain("test", 2);
        result.Should().Contain("text", 2);
        result.Should().Contain("here", 1);
        result.Should().Contain("question", 1);
        result.Should().Contain("it", 1);
        result.Should().Contain("any", 1);
        result.Should().Contain("good", 1);
    }
}