﻿using FileWordCounter.Factories;
using FileWordCounter.WordCounterLogic;

namespace FileWordCounter.FileWordCounters;

public class FileWordCounter : IFileWordCounter
{
    private readonly IWordCounter _wordCounter;
    
    public FileWordCounter(IWordCounterFactory wordCounterFactory)
    {
        _wordCounter = wordCounterFactory.Create();
    }
    
    public async Task<IReadOnlyDictionary<string, int>> CountWords(string filePath)
    {
        if (!File.Exists(filePath))
        {
            throw new ArgumentException($"File {filePath} does not exist");
        }
        
        var text = await File.ReadAllTextAsync(filePath);
        
        return _wordCounter.CountWords(text, Separators.DefaultSeparators);
    }
}