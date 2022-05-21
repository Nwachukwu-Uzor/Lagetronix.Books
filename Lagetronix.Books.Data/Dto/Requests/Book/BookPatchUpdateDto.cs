using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lagetronix.Books.Data.Dto.Requests.Book
{
    public class BookPatchUpdateDto
    {
        [StringLength(int.MaxValue, MinimumLength = 5)]
        public string Author { get; set; }

        [StringLength(int.MaxValue, MinimumLength = 5)]
        public string Title { get; set; }

        [StringLength(int.MaxValue, MinimumLength = 5)]
        public string Description { get; set; }
    }
}
