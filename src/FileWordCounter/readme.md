# File Word Counter solution

## How to run
There are two test files that can be used to run the application. They are in TestFiles folder. Those files are copied to the <output folder>/TestFiles folder on build. There are two ways to run the application. 
1. Run the application from output folder and provide the file paths as argument.
For example: .\FileWordCounter.exe text1.txt text2.txt
2. Run the application from Visual Studio or any other IDE and provide the file paths from the project settings (Application Arguments or Program Arguments).
## Assumptions 
There are a few assumptions that were considered when implementing this solution:
- File contains only English text so only one normalizer for English with a few rules/exceptions was implemented.  
- There are no restrictions on memory usage. So whole file can be read into memory in order to avoid sequence reading from file. This should save some time.
## Benchmarks
Benchmarks were implemented out of curiosity. I wanted to make sure that idea with word iterator is now slower that using standard Split method. Here are results:

| Method              | Mean      | Error    | StdDev   | Gen0    | Gen1   | Allocated |
|-------------------- |----------:|---------:|---------:|--------:|-------:|----------:|
| DotNetSplit         | 115.59 us | 2.252 us | 3.007 us | 14.1602 | 5.9814 | 174.77 KB |
| MySplitOnString     |  88.91 us | 1.327 us | 1.241 us | 11.4746 | 3.7842 | 142.13 KB |
| DefaultWordIterator |  96.18 us | 1.911 us | 2.919 us | 11.4746 | 3.6621 | 142.19 KB |
| MySplitOnByteArray  |  85.40 us | 0.421 us | 0.393 us | 11.4746 | 2.8076 | 142.27 KB |

