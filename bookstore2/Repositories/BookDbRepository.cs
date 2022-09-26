using System.Collections.Generic;
using System.Linq;
using bookstore2.Models;
using Microsoft.EntityFrameworkCore;

namespace bookstore2.Repositories
{
    public class BookDbRepository :IBookstoreRepository<Book>
    {
        BookStoreDbContext db;
        public BookDbRepository( BookStoreDbContext _db )
        {
            db = _db;
        }
        public void Add(Book entity)
        {
         db.Books.Add( entity );
         db.SaveChanges();
        }

        public void Delete(int id)
        {
            var book = Find(id);
            db.Books.Remove( book );
            db.SaveChanges();
        }

        public Book Find(int id)
        {
            var book = db.Books.Include(a=>a.Author).SingleOrDefault(b => b.Id == id);
            return book;
        }

        public IList<Book> List()
        {
            return db.Books.Include(a=>a.Author).ToList();
        }

        public void Update(int id, Book entity)
        {
          db.Books.Update(entity);
            db.SaveChanges();

        }
        public List<Book> Search( string searchtext ) 
        {
            var result = db.Books.Include(a=>a.Author).Where(b=>b.Title.Contains(searchtext)|| 
            b.Description.Contains(searchtext)||b.Author.FullName.Contains(searchtext)).ToList();
            return result;
        }
    }
}