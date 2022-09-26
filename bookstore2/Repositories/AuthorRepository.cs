using System.Collections.Generic;
using System.Linq;
using bookstore2.Models;

namespace bookstore2.Repositories
{
    public class AuthorRepository : IBookstoreRepository<Author>
    {
        List<Author> Authors;
        public AuthorRepository()
        {

            Authors = new List<Author>()
            {
                new Author { id = 1, FullName = " mo salah " },
                new Author { id = 2, FullName = " sadio mane " },
                new Author { id = 3, FullName = " Firmeneo " }
            };
        }
        public void Add(Author entity)
        {
            entity.id = Authors.Max(a => a.id) +1 ;
            Authors.Add(entity);
        }

        public void Delete(int id)
        {
            var author = Find(id);
            Authors.Remove(author);
        }

        public Author Find(int id)
        {
            var author = Authors.SingleOrDefault(a => a.id == id);
            return author;
        }

        public IList<Author> List()
        {
            return Authors;
        }

        public List<Author> Search(string searchtext)
        {
            return Authors.Where(a => a.FullName.Contains(searchtext)).ToList();  
        }

        public void Update( int id, Author entity)
        {
            var author = Find(entity.id);
            author.FullName = entity.FullName;
        
        }
    }
}
