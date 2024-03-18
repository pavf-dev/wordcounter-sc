using FileWordCounter.WordCounterLogic;

namespace FileWordCounter.Factories;

public interface IWordCounterFactory
{
    IWordCounter Create();
}