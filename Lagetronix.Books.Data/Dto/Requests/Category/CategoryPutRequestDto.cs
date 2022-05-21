using System.ComponentModel.DataAnnotations;

namespace Lagetronix.Books.Data.Dto.Requests.Category
{
    public class CategoryPutRequestDto
    {
        [Required]
        [StringLength(int.MaxValue, MinimumLength = 3)]
        public string Name { get; set; }

        [StringLength(int.MaxValue, MinimumLength = 3)]
        public string Description { get; set; }
    }
}
