using Lagetronix.Books.Data.Contracts;
using Lagetronix.Books.Data.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lagetronix.Books.Data.Repositories
{
    public class CategoriesRepositories : GenericRepository<Category>, ICategoriesRepository
    {
        public CategoriesRepositories(AppDbContext context) : base(context)
        {
        }
    }
}
