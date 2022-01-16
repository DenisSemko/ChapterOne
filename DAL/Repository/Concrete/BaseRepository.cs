using DAL.Repository.Abstract;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repository.Concrete
{
    public class BaseRepository<T> : IRepository<T> where T : class
    {
        public ApplicationContext myDbContext;

        public BaseRepository(ApplicationContext myDbContext)
        {
            this.myDbContext = myDbContext;
        }

        public void Save()
        {
            myDbContext.SaveChangesAsync();
        }
        public async Task<IEnumerable<T>> Get()
        {
            return await myDbContext.Set<T>().ToListAsync();
        }

        public async Task<T> GetById(Guid id)
        {
            return await myDbContext.Set<T>().FindAsync(id);
        }

        public async Task<T> Add(T entity)
        {
            await myDbContext.Set<T>().AddAsync(entity);
            await myDbContext.SaveChangesAsync();
            Save();
            return entity;
        }

        public async Task<T> Update(T entity)
        {
            myDbContext.Set<T>().Update(entity);
            await myDbContext.SaveChangesAsync();
            return entity;
        }

        public async Task<T> DeleteById(Guid id)
        {
            T entity = myDbContext.Set<T>().Find(id);
            myDbContext.Set<T>().Remove(entity);
            await myDbContext.SaveChangesAsync();
            return entity;
        }


    }
}
