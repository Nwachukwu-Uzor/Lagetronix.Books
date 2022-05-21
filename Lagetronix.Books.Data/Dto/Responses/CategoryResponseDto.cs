using System;

namespace Lagetronix.Books.Data.Dto.Responses
{
    public class CategoryResponseDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string CreatedAt { get; set; }
        public string ModifiedOn { get; set; }
    }
}
