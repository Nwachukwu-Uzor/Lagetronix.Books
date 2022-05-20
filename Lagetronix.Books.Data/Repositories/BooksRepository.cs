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
    public class BooksRepository : GenericRepository<Book>, IBooksRepository
    {
        public BooksRepository(AppDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Book>> GetBookAsync(int page, int size, bool includeCategory = false)
        {
            if (!includeCategory)
            {
                return await base.GetAllAsync(page, size);
            }

            return await _dbSet.Where(book => book.Status == 1)
                                .Include(book => book.Category)
                                .Skip((page - 1) * 1)
                                .Take(size)
                                .ToListAsync();
        }
    }
}
