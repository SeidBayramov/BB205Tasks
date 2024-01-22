using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomsMethod
{
    internal class Person
    {
        private string _fullName;
        private int _age;
        private string _phoneNumber;

        public string FullName
        {
            get { return _fullName; }

            set
            {

                string[] nameParts = value.Split(' ');
                if (nameParts.Length >= 2)
                {
                    if (char.IsUpper(nameParts[0][0]) && char.IsUpper(nameParts[nameParts.Length - 1][0]))
                    {
                        _fullName = value;
                    }
                    else
                    {
                        Console.WriteLine(("Both first and last names must start with a big letter."));
                    }
                }
                else
                {
                    Console.WriteLine(("Full name must consist of at least 2 words."));
                }
            }
        }

        public int Age
        {
            get { return _age; }
            set
            {
                if (value < 0)
                {
                    Console.WriteLine(("Age cannot be negative."));
                }
                _age = value;
            }
        }


        public string PhoneNumber
        {
            get { return _phoneNumber; }

            set { _phoneNumber = value; }
        }


        public static bool CustomContains(string source, string substring)
        {
            for (int i = 0; i <= source.Length - substring.Length; i++)
            {
                bool found = true;

                for (int j = 0; j < substring.Length; j++)
                {
                    if (source[i + j] != substring[j])
                    {
                        found = false;
                        break;
                    }
                }
                if (found)
                {
                    return true;
                }
            }
            return false;
        }


        public static string CustomReplace(string source, string oldValue, string newValue)
        {
            StringBuilder result = new StringBuilder();
            int startIndex = 0;

            while (startIndex < source.Length)
            {
                int nextIndex = source.IndexOf(oldValue, startIndex);

                if (nextIndex == -1)
                {
                    result.Append(source.Substring(startIndex));
                    break;
                }

                result.Append(source.Substring(startIndex, nextIndex - startIndex));
                result.Append(newValue);
                startIndex = nextIndex + oldValue.Length;
            }

            return result.ToString();
        }

        public static string CustomSubstring(string source, int startIndex, int length)
        {
            if (startIndex < 0)
            {
                Console.WriteLine(("startIndex", "Start index cannot be negative."));
            }
            if (startIndex >= source.Length)
            {
                Console.WriteLine(("startIndex", "Start index is out of range."));
            }
            if (length <= 0)
            {
                Console.WriteLine(("length", "Length must be greater than zero."));
            }

            int endIndex = startIndex + length;
            if (endIndex > source.Length)
            {
                Console.WriteLine(("length", "Substring length exceeds the length of the source string."));
            }

            StringBuilder substringBuilder = new StringBuilder(length);

            for (int i = startIndex; i < endIndex; i++)
            {
                substringBuilder.Append(source[i]);
            }

            return substringBuilder.ToString();
        }

        public static string CustomTrim(string input)
        {
            if (input == null)
            {
                Console.WriteLine(("input", "Input cannot be null."));
            }

            int startIndex = 0;
            int endIndex = input.Length - 1;

            while (startIndex <= endIndex && char.IsWhiteSpace(input[startIndex]))
            {
                startIndex++;
            }

            while (endIndex >= startIndex && char.IsWhiteSpace(input[endIndex]))
            {
                endIndex--;
            }

            if (startIndex > endIndex)
            {
              
                return string.Empty;
            }

            return input.Substring(startIndex, endIndex - startIndex + 1);
        }

    }
}

   
