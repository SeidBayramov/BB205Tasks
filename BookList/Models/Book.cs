using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp15.Models
{
    internal class Book
    {
        private static int BookCounter = 1000;
        public static int _id { get; set; }
        public int Id { get; set; }
        public string Name { get; set; }
        public string  AuthorName { get; set; }

        public string  BookCode { get; set; }


        public Book(string name,string authorName)
        {
            _id++;
            Id = _id;
            
            Name = name;
            AuthorName = authorName;
            BookCode = name.Substring(0,2).ToUpper()+BookCounter.ToString();
            
        }


    }
}
