using FileWordCounter.WordNormalizers;
using FluentAssertions;

namespace FileWordCounterTests;

public class DefaultEnglishWordNormalizerTests
{
    [Test]
    public void Normalizes_word_to_lower_case()
    {
        var normalizer = new DefaultEnglishWordNormalizer();
        
        var result = normalizer.Normalize("TeSt");
        
        result.Should().HaveCount(1);
        result.Should().Contain("test");
    }
    
    [Test]
    public void Normalizes_word_with_apostrophe_to_two_words()
    {
        var normalizer = new DefaultEnglishWordNormalizer();
        
        var result = normalizer.Normalize("we'll");
        
        result.Should().HaveCount(2);
        result.Should().Contain("we");
        result.Should().Contain("will");
    }
}