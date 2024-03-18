using FileWordCounter.Factories;
using FluentResults;

namespace FileWordCounter.FileWordCounters;

public class FilesWordCounter
{
    private readonly IFileWordCounterFactory _fileWordCounterFactory;

    public FilesWordCounter(IFileWordCounterFactory fileWordCounterFactory)
    {
        _fileWordCounterFactory = fileWordCounterFactory;
    }
    
    public async Task<Result<IReadOnlyDictionary<string, int>>> CountWords(string[] filePaths)
    {
        var tasks = filePaths.Select(filePath =>
        {
            var fileWordCounter = _fileWordCounterFactory.Create();
            
            return fileWordCounter.CountWords(filePath);
        });

        var whenAllTask = Task.WhenAll(tasks);

        try
        {
            var results = await whenAllTask;

            return Result.Ok(CombineResults(results));
        }
        catch (Exception e)
        {
            // TODO: Add logger so more details about exceptions stored for later investigation
            
            if (whenAllTask.Exception is not null)
            {
                return whenAllTask.Exception.InnerExceptions.Select(ex => new Error(ex.Message)).ToList();
            }
            
            return Result.Fail(e.Message);
        }
    }
    
    private static IReadOnlyDictionary<string, int> CombineResults(IReadOnlyCollection<IReadOnlyDictionary<string, int>> results)
    {
        if (results.Count == 1) return results.First();
    
        var combinedResults = new Dictionary<string, int>(results.Max(result => result.Count));

        foreach (var result in results)
        {
            foreach (var (word, count) in result)
            {
                if (!combinedResults.TryAdd(word, count))
                {
                    combinedResults[word] += count;
                }
            }
        }

        return combinedResults;
    }
}