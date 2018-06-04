using DataAccess;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{
    public abstract class BaseRepository<T> : IBaseRepository<T>
        where T : class
    {

        protected StoryEntities Context;

        public BaseRepository()
        {
            Context = new StoryEntities();
        }

        protected DbSet<T> DBSet
        {
            get
            {
                return Context.Set<T>();
            }
        }

        public void Create(T item)
        {
            Context.Set<T>().Add(item);
            Context.SaveChanges();
        }

        public List<T> GetAll()
        {
            return Context.Set<T>().ToList();
        }

        public T GetByID(int id)
        {
            return Context.Set<T>().Find(id);
        }


        public void Update(T item, Func<T, bool> findByIDPredacate)
        {
            var local = Context.Set<T>().Local.FirstOrDefault(findByIDPredacate);

            if (local != null)
            {
                Context.Entry(local).State = System.Data.Entity.EntityState.Detached;
            }
            Context.Entry(item).State = System.Data.Entity.EntityState.Modified;
            Context.SaveChanges();

        }

        public bool DeleteByID(int id)
        {
            bool isDeleted = false;
            T dbItem = Context.Set<T>().Find(id);
            if (dbItem != null)
            {
                Context.Set<T>().Remove(dbItem);
                int recordsChanged = Context.SaveChanges();
                isDeleted = recordsChanged > 0;
            }
            return isDeleted;
        }

        public abstract void Save(T item);

    }
}
