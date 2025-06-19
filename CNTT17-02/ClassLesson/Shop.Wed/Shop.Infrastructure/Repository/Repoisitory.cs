using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Infrastructure.Repository
{
    internal class Repoisitory
    {
        public class Repoisitory<T> : IRepository<T> where T : class
        {
            private readonly ShopDbContext_context
                private readonly DbSet<T>
            public Task AddAsync(T entity)
            {
                throw new NotImplementedException();
            }

            public void DeleteAsync(int id)
            {
                throw new NotImplementedException();
            }

            public Task<IEnumerable<T>> GetAllAsync()
            {
                throw new NotImplementedException();
            }

            public Task<Task>? GetByIdAsync(int id)
            {
                throw new NotImplementedException();
            }

            public Task saveChangeAsync()
            {
                throw new NotImplementedException();
            }

            public void UpdateAsync(T entity)
            {
                throw new NotImplementedException();
            }
        }
    }
