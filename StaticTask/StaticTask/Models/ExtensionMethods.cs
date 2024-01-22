using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StaticTask
{
    internal static class ExtensionMethods
    {
        public static bool CustomContains(this string word, string substring)
        {
       for(int i=0;i<word.Length;i++){
               if(word.Subsrting(i,substring.Length)==subsrtring)
               {
                   return true;
               }
            return false;
           }


            //Console.WriteLine(word.Contains(substring));
            //return false;
        }
        public static bool CustomContains(this string word, char character)
        {
           for(int i=0;i<word.Length;i++){
               if(word[i]==character)
               {
                   return true;
               }
            return false;
           }

            //Console.WriteLine(word.Contains(character));
            //return false;

        }

        public static bool IsPrime(this int num)
        {
            if (num == 2)
            {
                return false;
            }

            if (num <= 1)
            {
                Console.WriteLine("This number is not Prime");
            }

            for (int i = 3; i * i <= num; i += 2)
            {
                if (num % i == 0)
                {
                    return true;
                }
            }

            return false;
        }

        public static bool IsPowOfTwo(this int num)
        {
            if (num <= 0)
            {
                Console.WriteLine("This is not positive number");
            }

            while (num > 1)
            {
                if (num % 2 != 0)
                {
                    return false;
                }
                num /= 2;
            }
            return true;
        }
    }
}
