using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Years.Utils
{
    public class ParseqTests
    {
        public void DoIt()
        {
            var coords = "(5528.99,2008.78,1661.62)";
            coords.Parseq("(")
                .Parse(",", out float f1)
                .Parse(",", out float f2)
                .Parse(")", out float f3);

            Console.WriteLine($"{f1} {f2} {f3}");
        }
    }

    public static class ParseqExtensions
    {
        public static ParseqData Parseq(this string input, string initialDelimiter = null) => new ParseqData(input, initialDelimiter);
    }

    public class ParseqData
    {
        private readonly string _input;
        private int _index = 0;

        public ParseqData(string input, string initialDelimiter = null)
        {
            if (string.IsNullOrWhiteSpace(input))
            {
                throw new ArgumentException("Input can't be null or whitespace", nameof(input));
            }
            _input = input;

            if (!string.IsNullOrWhiteSpace(initialDelimiter))
            {
                _index = FindDelimiter(initialDelimiter, 0) + initialDelimiter.Length;
            }
        }
        
        public ParseqData Parse<T>(string delimiter, out T t)
        {
            t = default(T);
            
            if (string.IsNullOrWhiteSpace(delimiter))
            {
                throw new ArgumentException("Delimiter can't be null or empty", nameof(delimiter));
            }

            var endIndex = FindDelimiter(delimiter, _index);
            var length = endIndex - _index;
            var substr = _input.Substring(_index, length);

            t = (T)Convert.ChangeType(substr, typeof(T));
            //Console.WriteLine(substr);

            //progress to the start of the after-delimiter, which will be used as before delimiter in the next call
            _index = endIndex + delimiter.Length;

            return this;
        }

        //TODO: replace with Boyer Moore
        private int FindDelimiter(string delimiter, int startIndex)
        {
            for (int i = startIndex; i < _input.Length; i++)
            {
                if (_input.Substring(i, delimiter.Length).Equals(delimiter))
                {
                    return i;
                }
            }
            throw new ArgumentException($"Delimiter-before '{delimiter}' not found in after {startIndex} in {_input}");
        }
    }
}
