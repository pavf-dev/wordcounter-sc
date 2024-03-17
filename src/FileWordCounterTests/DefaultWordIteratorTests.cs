using FileWordCounter;
using FluentAssertions;

namespace FileWordCounterTests;

public class DefaultWordIteratorTests
{
    [Test]
    public void Returns_one_char_word_for_one_char_text()
    {
        var wordIterator = new DefaultWordIterator();
        wordIterator.Init("I", Separators.DefaultSeparators);
        
        var result = wordIterator.ToList();
        
        result.Should().HaveCount(1);
        result.Should().Contain("I");
    }
    
    [Test]
    public void Returns_one_word_for_one_word_text()
    {
        var wordIterator = new DefaultWordIterator();
        wordIterator.Init("test", Separators.DefaultSeparators);
        
        var result = wordIterator.ToList();
        
        result.Should().HaveCount(1);
        result.Should().Contain("test");
    }
    
    [Test]
    [TestCase(".")]
    [TestCase("...!?")]
    [TestCase("\n")]
    public void Returns_no_word_for_text_with_separators(string text)
    {
        var wordIterator = new DefaultWordIterator();
        wordIterator.Init(text, Separators.DefaultSeparators);
        
        var result = wordIterator.ToList();
        
        result.Should().HaveCount(0);
    }
    
    [Test]
    public void Returns_all_word_for_simple_text()
    {
        var wordIterator = new DefaultWordIterator();
        wordIterator.Init("Just a simple text", Separators.DefaultSeparators);
        
        var result = wordIterator.ToList();
        
        result.Should().HaveCount(4);
        result.Should().Contain("Just");
        result.Should().Contain("a");
        result.Should().Contain("simple");
        result.Should().Contain("text");
    }
    
    [Test]
    public void Handles_separator_at_the_end()
    {
        var wordIterator = new DefaultWordIterator();
        wordIterator.Init("Are those separators at the end!?", Separators.DefaultSeparators);
        
        var result = wordIterator.ToList();
        
        result.Should().HaveCount(6);
        result.Should().Contain("end");
    }
    
    [Test]
    public void Handles_separator_at_the_beginning()
    {
        var wordIterator = new DefaultWordIterator();
        wordIterator.Init("...story begins here", Separators.DefaultSeparators);
        
        var result = wordIterator.ToList();
        
        result.Should().HaveCount(3);
        result.Should().Contain("story");
    }
    
    [Test]
    public void Handles_few_separators_in_a_row()
    {
        var wordIterator = new DefaultWordIterator();
        wordIterator.Init("Example with ( weird ) text", Separators.DefaultSeparators);
        
        var result = wordIterator.ToList();
        
        result.Should().HaveCount(4);
        result.Should().Contain("with");
        result.Should().Contain("weird");
        result.Should().Contain("text");
    }

    [Test]
    public void Reset_method_resets_enumerator()
    {
        var wordIterator = new DefaultWordIterator();
        wordIterator.Init("Just a simple text", Separators.DefaultSeparators);
        
        wordIterator.ToList();
        wordIterator.Reset();
        var result = wordIterator.ToList();
        
        result.Should().HaveCount(4);
        result.Should().Contain("Just");
        result.Should().Contain("a");
        result.Should().Contain("simple");
        result.Should().Contain("text");
    }
}