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
        public async Task<T> AddAsync(T entity)
        {
            await _dbSet.AddAsync(entity);
            return await SaveChangesAsync() ? entity : null;
        }

        public async Task<bool> DeleteAsync(T entity)
        {
            entity.Status = 0;
            return await SaveChangesAsync();
        }

        public virtual async Task<IEnumerable<T>> GetAllAsync(int page, int size)
        {
            return await _dbSet.Where(entity => entity.Status == 1)
                         .AsNoTracking()
                         .Skip((page - 1) * size)
                         .Take(size)
                         .OrderByDescending(ent => ent.CreatedAt)
                         .ToListAsync();        
        }

        public virtual async Task<T> GetByIdAsync(Guid id)
        {
            return await _dbSet.Where(ent => ent.Id == id && ent.Status == 1)
                                .FirstOrDefaultAsync();
        }

        public async Task<bool> SaveChangesAsync()
        {
            var changes = await _context.SaveChangesAsync();
            return changes > 0;
        }

        public async Task<T> UpdateAsync(T entity)
        {
            _dbSet.Update(entity);
            return await SaveChangesAsync() ? entity : null;
        }
    }
}
