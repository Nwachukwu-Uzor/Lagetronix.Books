using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lagetronix.Books.Data.Dto.Requests.Category
{
    public class CategoryPatchUpdateDto
    {
        [StringLength(int.MaxValue, MinimumLength = 3)]
        public string Name { get; set; }

        [StringLength(int.MaxValue, MinimumLength = 3)]
        public string Description { get; set; }
    }
}
