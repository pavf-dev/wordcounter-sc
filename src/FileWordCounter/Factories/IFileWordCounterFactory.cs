using FileWordCounter.FileWordCounters;

namespace FileWordCounter.Factories;

public interface IFileWordCounterFactory
{
    IFileWordCounter Create();
}