using System.Collections;

namespace FileWordCounter.WordIterators;

public class DefaultWordIterator : IWordIterator
{
    private char[] _separators;
    private string _text;
    private int _currentPosition;
    private int _previousSeparatorPosition;

    
    
    public DefaultWordIterator()
    {
        _currentPosition = 0;
        _previousSeparatorPosition = -1;
        _text = string.Empty;
        _separators = Array.Empty<char>();
        // TODO: It doesn't consider culture specific cases like $123,456.78
        // TODO: It considers words with hyphens like "well-known" like one word
        // TODO: It considers "you're" as one word but "'something'" also is considered as a word although result must be "something"
    }

    public void Init(string text, char[] separators)
    {
        _text = text;
        _separators = separators;
    }
    
    public IEnumerator<string> GetEnumerator()
    {
        return this;
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    public bool MoveNext()
    {
        if (_currentPosition >= _text.Length)
        {
            return false;
        }

        while (_currentPosition < _text.Length)
        {
            var ch = _text[_currentPosition];
            
            if (char.IsLetter(ch) || char.IsNumber(ch) || !_separators.Contains(ch))
            {
                _currentPosition++;
                
                continue;
            }

            // Previous char was a separator and current is also separator
            if (_previousSeparatorPosition + 1 == _currentPosition)
            {
                _previousSeparatorPosition = _currentPosition;
                _currentPosition++;
            }
            else
            {
                Current = _text.Substring(_previousSeparatorPosition + 1, _currentPosition - _previousSeparatorPosition - 1);

                _previousSeparatorPosition = _currentPosition;
                _currentPosition++;

                return true;
            }
        }
        
        // The while loop ended and it didn't reach a separator at the end
        // so we have a word at the end of the text
        if (_previousSeparatorPosition + 1 != _currentPosition)
        {
            Current = _text.Substring(_previousSeparatorPosition + 1, _currentPosition - _previousSeparatorPosition - 1);

            return true;
        }

        return false;
    }

    public void Reset()
    {
        _currentPosition = 0;
        _previousSeparatorPosition = -1;
    }

    object IEnumerator.Current => Current;

    public string Current { get; private set; }

    public void Dispose()
    {
    }
}