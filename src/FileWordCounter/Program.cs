// See https://aka.ms/new-console-template for more information

using FileWordCounter;

var files = Environment.GetCommandLineArgs().Skip(1).ToArray();

var wordCounterFactory = new WordCounterFactory();
var fileWordCounterFactory = new FileWordCounterFactory(wordCounterFactory);
var filesWordCounter = new FilesWordCounter(fileWordCounterFactory);

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
    Console.WriteLine("File processing failed because of the following errors:");
    
    foreach (var error in result.Errors)
    {
        Console.WriteLine(error.Message);
    }
}
