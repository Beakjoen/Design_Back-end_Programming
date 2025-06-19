using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Shop.Domain.Interfaces;
using Shop.Infrastructure.Data;

namespace Shop.Infrastructure.Repositories
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly ShopDbContext _context;
        private readonly DbSet<T> _dbSet;
        public Repository(ShopDbContext context, DbSet<T> dbSet)
        {
            _context = context;
            _dbSet = dbSet;
        }

        public async Task AddAsync(T entity)=> await _context.AddAsync(entity);

        public void DeleteAsync(T entity) => _dbSet.Remove(entity);

        public async Task<IEnumerable<T>> GetAllAsync()=>await _dbSet.ToListAsync();

        public async Task<T>? GetByIdAsync(int id) => await _dbSet.FindAsync(id);
        
        public async Task SaveChangesAsync()=> await _context.SaveChangesAsync();
  
        public void UpdateAsync(T entity)=> _dbSet.Update(entity);
    }
}
