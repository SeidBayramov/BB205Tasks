using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp15.Models
{
    internal class Library
    {
        private static int _id =1000;
        public int Id { get; set; }
        public string Name { get; set; }

        public int BookList { get; set; }

        List<Book>books;
        public Library(string name)
        {
            _id++;
            Id = _id;
            Name = name;
            books = new List<Book>();
        }



        public void AddBook(Book book)
        {
             books.Add(book);
            Console.WriteLine($"{book.Name} {book.AuthorName}{book.Id}");

        }

        public void ShowAllBooks()
        {
            foreach (Book book in books)
            {
                Console.WriteLine($"{book.Name} {book.AuthorName}{book.Id}");
            }
        }

        public Book FindBookId(int id)
        {
            foreach (Book book in books)
            {
                if (book.Id == id)
                {
                  return book;
                }
            }
            return null;

        }

    }
}
