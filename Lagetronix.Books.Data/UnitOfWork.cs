using Lagetronix.Books.Data.Contracts;

namespace Lagetronix.Books.Data
{
    public class UnitOfWork : IUnitOfWork
    {
        public IBooksRepository Books { get; }

        public ICategoriesRepository Categories { get; }

        public UnitOfWork(IBooksRepository books, ICategoriesRepository categories)
        {
            Books = books;
            Categories = categories;
        }
    }
}
