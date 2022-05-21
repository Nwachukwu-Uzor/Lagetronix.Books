using AutoMapper;
using Lagetronix.Books.Data.Contracts;
using Lagetronix.Books.Data.Domain;
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
        public CategoriesController(IUnitOfWork unitOfWork, IMapper mapper)
        : base (unitOfWork, mapper)
        {

        }

        [HttpPost]
        public async Task<ActionResult<CategoryResponseDto>> CreateCategoryAsync(CategoryRegistrationDto categoryRegistrationDto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(null);
                }

                var category = _mapper.Map<Category>(categoryRegistrationDto);

                var categoryEntity = await _unitOfWork.Categories.AddAsync(category);

                if (category == null)
                {
                    return BadRequest(category);
                }

                var categoryToReturn = _mapper.Map<CategoryResponseDto>(categoryEntity);

                return CreatedAtRoute(
                    nameof(GetCategoryById),
                    new { categoryId = categoryEntity.Id },
                    categoryToReturn
                ); ;

            } catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CategoryResponseDto>>> GetAllCategoriesAsync(int page = 1, int size = 20)
        {
            try
            {
                var categories = await _unitOfWork.Categories.GetAllAsync(page, size);

                return Ok(_mapper.Map<IEnumerable<CategoryResponseDto>>(categories));
            } catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{categoryId:Guid}", Name = nameof(GetCategoryById))]
        public async Task<ActionResult<CategoryResponseDto>> GetCategoryById(Guid categoryId)
        {
            try
            {
                var category = await _unitOfWork.Categories.GetByIdAsync(categoryId);

                if (category == null)
                {
                    return NotFound(null);
                }

                return Ok(_mapper.Map<CategoryResponseDto>(category));

            } catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
