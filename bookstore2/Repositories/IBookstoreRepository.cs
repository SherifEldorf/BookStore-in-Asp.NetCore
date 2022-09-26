using System.Collections.Generic;

namespace bookstore2.Repositories
{
    public interface IBookstoreRepository<TEntity>
    {
        IList<TEntity> List();
        TEntity Find(int id);
        void Add(TEntity entity);
        void Update(int id, TEntity entity);
        void Delete(int id);
        List<TEntity> Search(string searchtext);


    }
}
