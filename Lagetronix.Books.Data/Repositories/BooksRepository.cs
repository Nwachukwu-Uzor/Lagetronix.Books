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

        public async Task<IEnumerable<Book>> GetAllBooksAsync(int page, int size, bool includeCategory = true)
        {
            var books = _dbSet.Where(book => book.Status == 1 && book.Category.Status == 1);
            if (includeCategory)
            {
               books = books.Include(book => book.Category);
            }

            return await books
                            .Skip((page - 1) * 1)
                            .Take(size)
                            .OrderByDescending(ent => ent.CreatedAt)
                            .ToListAsync();
        }

        public async override Task<Book> GetByIdAsync(Guid id)
        {
            return await _dbSet.Where(book => book.Id == id && book.Status == 1 && book.Category.Status == 1).Include(book => book.Category).FirstAsync();
        }

        public async Task<IEnumerable<Book>> GetFavoriteBooks(int page, int size, bool includeCategory = false)
        {
            return await _dbSet.Where(book => book.Status == 1 && book.IsFavorite && book.Category.Status == 1)
                               .Include(book => book.Category)
                               .Skip((page - 1) * 1)
                               .Take(size)
                               .OrderByDescending(ent => ent.CreatedAt)
                               .ToListAsync();
        }
    }
}
