// See https://aka.ms/new-console-template for more information

using FileWordCounter;

var files = Environment.GetCommandLineArgs();
var wordCounterFabric = new WordCounterFabric();
var filesWordCounter = new FilesWordCounter(wordCounterFabric);

var result = await filesWordCounter.CountWords(files);

if (result.IsSuccess)
{
    foreach (var (word, count) in result.Value)
    {
        Console.WriteLine($"{word}: {count}");
    }
}
else
{
    foreach (var error in result.Errors)
    {
        Console.WriteLine(error.Message);
    }
}
