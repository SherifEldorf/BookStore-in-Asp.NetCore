using System.Collections.Generic;
using System.Linq;
using bookstore2.Models;

namespace bookstore2.Repositories
{
    public class AuthorDbRepository :IBookstoreRepository<Author>
    {
           BookStoreDbContext db;
        public AuthorDbRepository( BookStoreDbContext _db )
        {
            db = _db;
          
        }
        public void Add(Author entity)
        {
            db.Authors.Add(entity);
            db.SaveChanges();

        }

        public void Delete(int id)
        {
            var author = Find(id);
            db.Authors.Remove(author);
            db.SaveChanges();

        }

        public Author Find(int id)
        {
            var author = db.Authors.SingleOrDefault(a => a.id == id);
            return author;
        }

        public IList<Author> List()
        {
            return db.Authors.ToList();
        }

        public List<Author> Search(string searchtext)
        {
            return db.Authors.Where(a => a.FullName.Contains(searchtext)).ToList();
        }

        public void Update( int id, Author entity)
        {
            db.Authors.Update( entity);
            db.SaveChanges();

        }
    }
}
