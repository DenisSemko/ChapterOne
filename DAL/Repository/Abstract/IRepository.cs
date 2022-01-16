using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repository.Abstract
{
    public interface IRepository<T> where T : class
    {
        public Task<IEnumerable<T>> Get();
        public Task<T> GetById(Guid id);
        public Task<T> Add(T item);
        public Task<T> Update(T item);
        public Task<T> DeleteById(Guid id);
        public void Save();
    }
}
