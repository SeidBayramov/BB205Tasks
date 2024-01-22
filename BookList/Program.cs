using ConsoleApp15.Models;

namespace ConsoleApp15
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Library library = new Library("MIlli kitabxana");
            Book book = new Book("Suc ve Ceza", "Dosteyevski");
            Book book2 = new Book("Varli ata kasib ata", "Cinli bir adam");
            library.AddBook(book);
            library.AddBook(book2);
      
            library.FindBookId(1);
            library.ShowAllBooks();


        }
    }
}