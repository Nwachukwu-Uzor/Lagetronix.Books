using Lagetronix.Books.Data.Contracts;
using Lagetronix.Books.Data.Domain;

namespace Lagetronix.Books.Data.Repositories
{
    public class CategoriesRepository : GenericRepository<Category>, ICategoriesRepository
    {
        public CategoriesRepository(AppDbContext context) : base(context)
        {
        }
    }
}
