namespace FileWordCounter;

public interface IFileWordCounterFactory
{
    FileWordCounter Create();
}

public class FileWordCounterFactory : IFileWordCounterFactory
{
    private readonly IWordCounterFactory _wordCounterFactory;
    
    public FileWordCounterFactory(IWordCounterFactory wordCounterFactory)
    {
        _wordCounterFactory = wordCounterFactory;
    }
    
    public FileWordCounter Create()
    {
        return new FileWordCounter(_wordCounterFactory);
    }
}