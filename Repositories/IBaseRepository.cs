using DataAccess;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{
    public interface IBaseRepository<T>  where T : class
    {
        List<T> GetAll();

        T GetByID(int id);

        void Save(T item);

        void Create(T item);

        void Update(T item, Func<T, bool> findByIDPredecate);
    }
}
