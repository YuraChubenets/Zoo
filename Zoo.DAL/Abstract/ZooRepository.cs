using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using Zoo.BLL.Entities;

namespace Zoo.DAL.Abstract
{
   public  class ZooRepository<T> : IRepository<T>  where T : class ,new () 
    {
       //singleton
       private readonly ZooDbContext context = ZooDbContext.Instance;

       public IQueryable<T> GetAll
       {
           get
           {
               if (typeof(T) == typeof(Animal))
               {
                   return this.context.Animals as IQueryable<T>;
               }
               else if (typeof(T) == typeof(Department))
               {
                   return this.context.Departments as IQueryable<T>;
               }
               else if (typeof(T) == typeof(Gender))
               {
                   return this.context.Genders as IQueryable<T>;
               }
               else if (typeof(T) == typeof(Role))
               {
                   return this.context.Roles as IQueryable<T>;
               }
               else if (typeof(T) == typeof(User))
               {
                   return this.context.Users as IQueryable<T>;
               }
               else if (typeof(T) == typeof(Feeding))
               {
                   return this.context.Feeding as IQueryable<T>;
               }
               else if (typeof(T) == typeof(Lifecycle))
               {
                   return this.context.Lifecycles as IQueryable<T>;
               }
               else if (typeof(T) == typeof(ATD))
               {
                   return this.context.ATDs as IQueryable<T>;
               }
               else
               {
                   throw new NotSupportedException("/b This type is not supported");
               }

           }
       }

        public T GetOne(int id)
        {
            return this.context.Set<T>().Find(id);
        }

        public void Create(T item)
        {
            try
            {
                this.context.Set<T>().Add(item);
                this.context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Update(T item)
        {
            this.context.Entry<T>(item).State = EntityState.Modified;
            this.context.SaveChanges();
        }

        public void Delete(int id)
        {
            var user = this.context.Set<T>().Find(id);
            if (user != null)
            {
                this.context.Set<T>().Remove(user);
                this.context.SaveChanges();
            }
        }
    }
}
