using Microsoft.Extensions.Primitives;

namespace FileWordCounter.WordNormalizers;

public interface IWordNormalizer
{
    StringValues Normalize(string word);
}