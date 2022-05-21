using System;

namespace Lagetronix.Books.Data.Dto.Responses
{
    public class BookResponseDto
    {
        public Guid Id { get; set; }
        public string Author { get; set; }
        public string Title { get; set; }
        public string Category { get; set; }
        public bool IsFavorite { get; set; }
        public string Description { get; set; }
        public string CreatedAt { get; set; }
        public string ModifiedOn { get; set; }
    }
}
