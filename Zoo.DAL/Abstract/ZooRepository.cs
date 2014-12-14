using System;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Zoo.DAL.Abstract
{
   public  class ZooRepository<T> : IRepository<T>  where T : class 
    {
       //singleton
       private readonly ZooDbContext context = ZooDbContext.Instance;


       public IQueryable<T> GetAll
       {
           get
           {
              return this.context.Set<T>();
           }
       }

        public async Task<T> GetOne(int id)
        {
            return await this.context.Set<T>().FindAsync(id);
        }

        public async Task Create(T item)
        {
            try
            {
                this.context.Set<T>().Add(item);
                await this.context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task Update(T item, int key) 
        {
            T exist =  this.context.Set<T>().Find(key);
            this.context.Entry(exist).CurrentValues.SetValues(item);
            this.context.Entry<T>(exist).State = EntityState.Modified;
          await  this.context.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            var user = this.context.Set<T>().Find(id);
            if (user != null)
            {
                this.context.Set<T>().Remove(user);
                await this.context.SaveChangesAsync();
            }
        }
    }
}
