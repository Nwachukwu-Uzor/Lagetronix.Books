namespace Lagetronix.Books.Data.Contracts
{
    public interface IUnitOfWork
    {
        public IBooksRepository Books { get; }
        public ICategoriesRepository Categories { get; }
    }
}
