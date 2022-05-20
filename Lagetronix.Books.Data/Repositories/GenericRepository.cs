using Lagetronix.Books.Data.Contracts;
using Lagetronix.Books.Data.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lagetronix.Books.Data.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity
    {
        protected readonly DbSet<T> _dbSet;
        private readonly AppDbContext _context;
        public GenericRepository(AppDbContext context)
        {
            _dbSet = context.Set<T>();
            _context = context;
        }
        public async Task<bool> AddAsync(T entity)
        {
            await _dbSet.AddAsync(entity);
            return await SaveChangesAsync();
        }

        public async Task<bool> DeleteAsync(T entity)
        {
            _dbSet.Remove(entity);
            return await SaveChangesAsync();
        }

        public async Task<IEnumerable<T>> GetAllAsync(int page, int size)
        {
            return await _dbSet.Where(entity => entity.Status == 1)
                         .AsNoTracking()
                         .Skip((page - 1) * size)
                         .Take(size)
                         .ToListAsync();        
        }

        public async Task<T> GetByIdAsync(Guid id)
        {
            return await _dbSet.Where(ent => ent.Id == id && ent.Status == 1)
                                .FirstOrDefaultAsync();
        }

        public async Task<bool> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> UpdateAsync(T entity)
        {
            _dbSet.Update(entity);
            return await SaveChangesAsync();
        }
    }
}
