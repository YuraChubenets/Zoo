using System;
using System.Collections.Generic;
using System.Linq;

namespace Zoo.DAL.Abstract
{
   public interface IRepository<T> : IDisposable  where T:class
    {
        IQueryable<T> GetAll { get; }
        T GetOne(int id);
        void Create(T item);
        void Update(T item);
        void Delete(int id);
       
    }
}
