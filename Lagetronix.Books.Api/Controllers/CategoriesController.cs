using AutoMapper;
using Lagetronix.Books.Api.Response;
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
        public async Task<ActionResult<ApiResponse<CategoryResponseDto>>> CreateCategoryAsync(CategoryRegistrationDto categoryRegistrationDto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(
                        ApiResponse<CategoryResponseDto>.FailureResponse(new List<string> { "Invalid model properties" }) 
                    );
                }

                var category = _mapper.Map<Category>(categoryRegistrationDto);

                var categoryEntity = await _unitOfWork.Categories.AddAsync(category);

                if (category == null)
                {
                    return BadRequest(
                        ApiResponse<CategoryResponseDto>.FailureResponse(new List<string> { "Unable to create category" })
                    );
                }

                var categoryToReturn = _mapper.Map<CategoryResponseDto>(categoryEntity);

                return CreatedAtRoute(
                    nameof(GetCategoryById),
                    new { categoryId = categoryEntity.Id },
                    ApiResponse<CategoryResponseDto>.SuccessResponse(categoryToReturn)
                ); 

            } catch(Exception ex)
            {
                return BadRequest(
                    ApiResponse<CategoryResponseDto>.FailureResponse(new List<string> { ex.Message })
                );
            }
        }

        [HttpGet]
        public async Task<ActionResult<ApiResponse<IEnumerable<CategoryResponseDto>>>> GetAllCategoriesAsync(int page = 1, int size = 20)
        {
            try
            {
                var categories = await _unitOfWork.Categories.GetAllAsync(page, size);

                var categoriesToReturn = _mapper.Map<IEnumerable<CategoryResponseDto>>(categories);
                return Ok(
                    ApiResponse<IEnumerable<CategoryResponseDto>>.SuccessResponse(categoriesToReturn)
                );
            } catch(Exception ex)
            {
                return BadRequest(
                    ApiResponse<IEnumerable<CategoryResponseDto>>.FailureResponse(new List<string> { ex.Message })
                );
            }
        }

        [HttpGet("{categoryId:Guid}", Name = nameof(GetCategoryById))]
        public async Task<ActionResult<ApiResponse<CategoryResponseDto>>> GetCategoryById(Guid categoryId)
        {
            try
            {
                var category = await _unitOfWork.Categories.GetByIdAsync(categoryId);

                if (category == null)
                {
                    return NotFound(
                        ApiResponse<CategoryResponseDto>.FailureResponse(new List<string> { "No category with the Id provided" })
                    );
                }

                var categoryToReturn = _mapper.Map<CategoryResponseDto>(category);

                return Ok(
                    ApiResponse<CategoryResponseDto>.SuccessResponse(categoryToReturn)
                );

            } catch(Exception ex)
            {
                return BadRequest(
                        ApiResponse<CategoryResponseDto>.FailureResponse(new List<string> { ex.Message }) 
                );
            }
        }
    }
}
