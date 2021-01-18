using System.Collections.Generic;

namespace starteAlkemy.Repository
{
    public interface IRepository<TEntity> where TEntity : class
    {
        TEntity Get();
       
        void Add(TEntity entity);
        void Edit(TEntity entity);
        void Delete(int id);
        void Save();
    }
}
