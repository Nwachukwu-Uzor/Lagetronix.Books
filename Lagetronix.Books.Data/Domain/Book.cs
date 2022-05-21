using System;

namespace Lagetronix.Books.Data.Domain
{
    public class Book : BaseEntity
    {
        public string Author { get; set; }
        public string Title { get; set; }
        public Guid CategoryId { get; set; }
        public Category Category { get; set; }
        public bool IsFavorite { get; set; } = false;
        public string Description { get; set; }
    }
}
