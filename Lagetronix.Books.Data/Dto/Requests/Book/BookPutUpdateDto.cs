using System.ComponentModel.DataAnnotations;

namespace Lagetronix.Books.Data.Dto.Requests.Book
{
    public class BookPutUpdateDto
    {
        [Required]
        [StringLength(int.MaxValue, MinimumLength = 5)]
        public string Author { get; set; }

        [Required]
        [StringLength(int.MaxValue, MinimumLength = 5)]
        public string Title { get; set; }


        [StringLength(int.MaxValue, MinimumLength = 5)]
        public string Description { get; set; }
    }
}
