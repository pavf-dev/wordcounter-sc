using BenchmarkDotNet.Attributes;
using FileWordCounter.WordIterators;

namespace FileWordCounterBenchmarks;

[MemoryDiagnoser]
public class SplitImplementationBenchmarks
{
    private string _testText;
    private byte[] _testTextAsByteArray;
    private readonly char[] _splitters = new[] { ' ', '.', ',', '!', '?', '"', '(', ')', ':', ';', '\n', '\t', '\r' };

    [GlobalSetup]
    public void Setup()
    {
        _testText = File.ReadAllText("test-text.txt");
        _testTextAsByteArray = File.ReadAllBytes("test-text.txt");
    }

    [Benchmark]
    public Dictionary<string, int> DotNetSplit()
    {
        var result = new Dictionary<string, int>(_testText.Length / 5);
        var words = _testText.Split(_splitters, StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);

        foreach (var word in words)
        {
            if (!result.TryAdd(word, 1))
            {
                result[word]++;
            }
        }

        return result;
    }

    [Benchmark]
    public Dictionary<string, int> MySplitOnString()
    {
        int position = 0;
        int lastSplitterPosition = 0;
        var result = new Dictionary<string, int>(_testText.Length / 5);
        
        foreach (var ch in _testText)
        {
            if (!(char.IsLetter(ch) || char.IsNumber(ch)) && _splitters.Contains(ch))
            {
                if (lastSplitterPosition + 1  == position)
                {
                    lastSplitterPosition = position;
                }
                else
                {
                    var word = _testText.Substring(lastSplitterPosition + 1, position - lastSplitterPosition - 1);
                    if (!result.TryAdd(word, 1))
                    {
                        result[word]++;
                    }
                    
                    lastSplitterPosition = position;    
                }
            }
            
            position++;
        }

        if (lastSplitterPosition + 1 != position)
        {
            var word = _testText.Substring(lastSplitterPosition + 1, position - lastSplitterPosition - 1);
            
            if (!result.TryAdd(word, 1))
            {
                result[word]++;
            }
        }

        return result;
    }

    [Benchmark]
    public Dictionary<string, int> DefaultWordIterator()
    {
        var iterator = new DefaultWordIterator();
        iterator.Init(_testText, _splitters);
        var result = new Dictionary<string, int>(_testText.Length / 5);

        foreach (var word in iterator)
        {
            if (!result.TryAdd(word, 1))
            {
                result[word]++;
            }
        }

        return result;
    }
    
    [Benchmark]
    public Dictionary<string, int> MySplitOnByteArray()
    {
        int position = 0;
        int lastSpliterPosition = 0;
        var result = new Dictionary<string, int>(_testText.Length / 5);
        
        foreach (var b in _testTextAsByteArray)
        {
            if (!(b is >= 65 and <= 90 or >= 97 and <= 122 or >= 48 and <= 57) &&
                b is (byte)' ' or (byte)'.' or (byte)',' or (byte)'!' or (byte)'?' or (byte)'(' or (byte)')' or (byte)':' or (byte)';' or (byte)'\n' or (byte)'\t' or (byte)'\r')
            {
                if (lastSpliterPosition + 1  == position)
                {
                    lastSpliterPosition = position;
                }
                else
                {
                    var word = System.Text.Encoding.UTF8.GetString(_testTextAsByteArray.AsSpan(lastSpliterPosition + 1, position - lastSpliterPosition - 1));
                    if (!result.TryAdd(word, 1))
                    {
                        result[word]++;
                    }
                    
                    lastSpliterPosition = position;    
                }
            }
            
            position++;
        }

        if (lastSpliterPosition + 1 != position)
        {
            var word = System.Text.Encoding.UTF8.GetString(_testTextAsByteArray.AsSpan(lastSpliterPosition + 1, position - lastSpliterPosition - 1));
            
            if (!result.TryAdd(word, 1))
            {
                result[word]++;
            }
        }

        return result;
    }
}