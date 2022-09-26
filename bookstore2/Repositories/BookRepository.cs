using System.Collections.Generic;
using System.Linq;
using bookstore2.Models;

namespace bookstore2.Repositories
{
    public class BookRepository : IBookstoreRepository<Book>
    {
        List<Book> books;
        public BookRepository()
        {
            books = new List<Book>() {

                new Book
                {
                    Id = 1,Title="C#",Description="no description",Author=new Author{id=2}
                },
                new Book
                {
                    Id = 2,
                    Title = "Java scripte",
                    Description = "no Data",Author=new Author{id=2}
                },
                new Book
                {
                    Id = 3,Title="python",Description="no need",Author=new Author{id=3}
                }

        };
        }
        public void Add(Book entity)
        {
            entity.Id = books.Max(b => b.Id) + 1;
            books.Add(entity);
        }

        public void Delete(int id)
        {
            var book = Find(id);
            books.Remove(book);
        }

        public Book Find(int id)
        {
            var book = books.SingleOrDefault(b => b.Id == id);
            return book;
         }

        public IList<Book> List()
        {
            return books;
        }

        public List<Book> Search(string searchtext)
        {
            return books.Where(a => a.Title.Contains(searchtext)).ToList();
        }

        public void Update(int id, Book entity)
        {
            var book=Find(entity.Id);
            book.Title = entity.Title;
            book.Description = entity.Description;
            book.Author = entity.Author;
            book.ImageUrl=entity.ImageUrl;  

        }
    }
}
