using System;
using System.ComponentModel.DataAnnotations;

namespace Lagetronix.Books.Data.Dto.Requests
{
    public class BookRegistrationDto
    {
        [Required]
        [StringLength(int.MaxValue, MinimumLength = 5)]
        public string Author { get; set; }

        [Required]
        [StringLength(int.MaxValue, MinimumLength = 5)]
        public string Title { get; set; }
        
        [Required]
        public Guid CategoryId { get; set; }

        [StringLength(int.MaxValue, MinimumLength = 5)]
        public string Description { get; set; }
    }
}
