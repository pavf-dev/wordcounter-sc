using FileWordCounter.Factories;
using FileWordCounter.FileWordCounters;
using FluentAssertions;
using NSubstitute;

namespace FileWordCounterTests;

public class FilesWordCounterTests
{
    [Test]
    public async Task Returns_all_errors_from_all_failed_counters()
    {
        var fileWordCounterFactory = Substitute.For<IFileWordCounterFactory>();
        var fileWordCounter = Substitute.For<IFileWordCounter>();
        var task = Task.FromException<IReadOnlyDictionary<string, int>>(new ArgumentException("File <file-name> doesn't exist"));
        fileWordCounter.CountWords(Arg.Any<string>()).Returns(task);
        fileWordCounterFactory.Create().Returns(fileWordCounter);
        
        // Act
        var filesWordCounter = new FilesWordCounter(fileWordCounterFactory);
        var result = await filesWordCounter.CountWords(["file1", "file2"]);

        result.Errors.Should().HaveCount(2);
    }
    
    [Test]
    public async Task Result_has_only_one_error_if_only_one_counter_fails()
    {
        var fileWordCounterFactory = Substitute.For<IFileWordCounterFactory>();
        var fileWordCounter = Substitute.For<IFileWordCounter>();
        var task = Task.FromException<IReadOnlyDictionary<string, int>>(new ArgumentException("File 'file1' doesn't exist"));
        fileWordCounter.CountWords(Arg.Is<string>(x => x == "file1")).Returns(task);
        fileWordCounter.CountWords(Arg.Is<string>(x => x == "file2")).Returns(new Dictionary<string, int> {{"word", 1}});
        fileWordCounterFactory.Create().Returns(fileWordCounter);
        
        // Act
        var filesWordCounter = new FilesWordCounter(fileWordCounterFactory);
        var result = await filesWordCounter.CountWords(["file1", "file2"]);

        result.Errors.Should().HaveCount(1);
    }
    
    [Test]
    public async Task Combines_file_counting_results_into_one_correct_count_result()
    {
        var fileWordCounterFactory = Substitute.For<IFileWordCounterFactory>();
        var fileWordCounter = Substitute.For<IFileWordCounter>();
        fileWordCounter.CountWords(Arg.Is<string>(x => x == "file1")).Returns(CreateResultForFile1());
        fileWordCounter.CountWords(Arg.Is<string>(x => x == "file2")).Returns(CreateResultForFile2());
        fileWordCounterFactory.Create().Returns(fileWordCounter);
        
        // Act
        var filesWordCounter = new FilesWordCounter(fileWordCounterFactory);
        var result = await filesWordCounter.CountWords(["file1", "file2"]);

        result.Errors.Should().HaveCount(0);
        var counter = result.Value;
        counter.Should().HaveCount(10);
        counter.Should().Contain("this", 1);
        counter.Should().Contain("is", 3);
        counter.Should().Contain("a", 2);
        counter.Should().Contain("test", 2);
        counter.Should().Contain("text", 2);
        counter.Should().Contain("here", 1);
        counter.Should().Contain("question", 1);
        counter.Should().Contain("it", 1);
        counter.Should().Contain("any", 1);
        counter.Should().Contain("good", 1);
    }

    private IReadOnlyDictionary<string, int> CreateResultForFile1()
    {
        return new Dictionary<string, int>
        {
            {"this", 1},
            {"is", 1},
            {"a", 1},
            {"test", 1},
            {"text", 1}
        };
    }
    
    private IReadOnlyDictionary<string, int> CreateResultForFile2()
    {
        return new Dictionary<string, int>
        {
            {"here", 1},
            {"is", 2},
            {"a", 1},
            {"question", 1},
            {"it", 1},
            {"any", 1},
            {"good", 1},
            {"test", 1},
            {"text", 1}
        };
    }
}