using FileWordCounter.FileWordCounters;

namespace FileWordCounter.Factories;

public class FileWordCounterFactory : IFileWordCounterFactory
{
    private readonly IWordCounterFactory _wordCounterFactory;
    
    public FileWordCounterFactory(IWordCounterFactory wordCounterFactory)
    {
        _wordCounterFactory = wordCounterFactory;
    }
    
    public IFileWordCounter Create()
    {
        return new FileWordCounters.FileWordCounter(_wordCounterFactory);
    }
}