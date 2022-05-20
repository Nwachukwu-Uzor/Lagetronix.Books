using System;

namespace Lagetronix.Books.Data.Dto.Requests
{
    public class CategoryRegistrationDto
    {
        public string Author { get; set; }
        public string Title { get; set; }
        public Guid CategoryId { get; set; }
        public string Description { get; set; }
    }
}
