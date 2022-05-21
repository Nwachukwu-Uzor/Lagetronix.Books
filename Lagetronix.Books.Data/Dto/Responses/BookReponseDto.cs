using System;

namespace Lagetronix.Books.Data.Dto.Responses
{
    public class BookReponseDto
    {
        public Guid Id { get; set; }
        public string Author { get; set; }
        public string Title { get; set; }
        public string Category { get; set; }
        public bool IsFavorite { get; set; }
        public string Description { get; set; }
    }
}
