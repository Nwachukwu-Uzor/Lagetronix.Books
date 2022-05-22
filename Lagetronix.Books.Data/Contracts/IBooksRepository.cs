using Lagetronix.Books.Data.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lagetronix.Books.Data.Contracts
{
    public interface IBooksRepository : IGenericRepository<Book>
    {
        Task<IEnumerable<Book>> GetAllBooksAsync(int page, int size, bool includeCategory = false);
        Task<IEnumerable<Book>> GetFavoriteBooks(int page, int size, bool includeCategory = false);
        Task<IEnumerable<Book>> GetBooksByCategory(Guid categoryId, int page, int size, bool includeCategory = false);
    }
}
