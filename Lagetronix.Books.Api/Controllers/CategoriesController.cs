using Lagetronix.Books.Data.Dto.Requests;
using Lagetronix.Books.Data.Dto.Responses;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lagetronix.Books.Api.Controllers
{
    public class CategoriesController : BaseController
    {
        [HttpPost]
        public async Task<ActionResult<CategoryResponseDto>> CreateCategoryAsync(CategoryRegistrationDto categoryRegistrationDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(null);
            }
            try
            {

            } catch(Exception)
            {

            }
        }
    }
}
