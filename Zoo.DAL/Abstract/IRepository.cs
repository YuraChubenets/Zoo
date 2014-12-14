using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Zoo.DAL.Abstract
{
   public interface IRepository<T>  where T:class
    {
        IQueryable<T> GetAll { get; }
        Task<T> GetOne(int id);
        Task Create(T item);
        Task Update(T item, int key);
        Task Delete(int id);
    }
}
