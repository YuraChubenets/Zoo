using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Zoo.DAL.Abstract
{
    interface IRepository<T> : IDisposable  where T:class
    {
        IEnumerable<T> GetAnimalList();
        T GetAnimal(int id);
        void Create(T item);
        void Update(T item);
        void Delete(int id);
        void Save();

    }
}
