using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Infrastructure.Repository
{
    public interface IRepository<T> where : class
    {
    }
    Task<Task>? GetByIdAsync(int id);
        Task<IEnumerable<T>> GetAllAsync();
        Task AddAsync (T entity);
        void UpdateAsync(T entity);
        void DeleteAsync(int id);
        Task saveChangeAsync();

}
