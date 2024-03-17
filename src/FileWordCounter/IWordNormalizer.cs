using Microsoft.Extensions.Primitives;

namespace FileWordCounter;

public interface IWordNormalizer
{
    StringValues Normalize(string word);
}

public class DefaultEnglishWordNormalizer : IWordNormalizer
{
    public StringValues Normalize(string word)
    {
        if (string.IsNullOrWhiteSpace(word)) throw new ArgumentException($"Parameter {nameof(word)} can not be null or empty");
        
        if (word == "I") return word;

        if (word.EndsWith("'ll"))
        {
            return new StringValues(new[]
            {
                // TODO: First part might be "I" which should not be lowered
                word.Replace("'ll", string.Empty).ToLowerInvariant(),
                "will"
            });
        }

        return word.ToLowerInvariant();
    }
}