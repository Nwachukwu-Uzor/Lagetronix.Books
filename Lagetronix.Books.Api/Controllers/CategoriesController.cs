using AutoMapper;
using Lagetronix.Books.Api.Response;
using Lagetronix.Books.Data.Contracts;
using Lagetronix.Books.Data.Domain;
using Lagetronix.Books.Data.Dto.Requests;
using Lagetronix.Books.Data.Dto.Requests.Category;
using Lagetronix.Books.Data.Dto.Responses;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
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

        [HttpPut("{categoryId:Guid}")]
        public async Task<ActionResult<ApiResponse<BookResponseDto>>> UpdateBook(Guid categoryId, CategoryPutRequestDto categoryUpdateDto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(
                        ApiResponse<BookResponseDto>
                            .FailureResponse(new List<string> { "Invalid data for update" })
                    );
                }

                var categoryEntity = await _unitOfWork.Categories.GetByIdAsync(categoryId);

                if (categoryEntity == null)
                {
                    return NotFound(
                         ApiResponse<CategoryResponseDto>
                            .FailureResponse(new List<string> { "No category with the specified id" })
                    );
                }

                var categoryUpdateEntity = _mapper.Map(categoryUpdateDto, categoryEntity);

                var updatedCategory = await _unitOfWork.Categories.UpdateAsync(categoryUpdateEntity);

                if (updatedCategory == null)
                {
                    return BadRequest(
                        ApiResponse<CategoryResponseDto>
                            .FailureResponse(new List<string> { "Invalid model properties" })
                    );
                }

                var categoryToReturn = _mapper.Map<CategoryResponseDto>(updatedCategory);

                return Ok(
                    ApiResponse<CategoryResponseDto>.SuccessResponse(categoryToReturn)
                );

            }
            catch (Exception ex)
            {
                return BadRequest(
                   ApiResponse<CategoryResponseDto>
                   .FailureResponse(new List<string> { ex.Message })
               );
            }
        }

        [HttpPatch("{categoryId:Guid}")]
        public async Task<ActionResult<ApiResponse<CategoryResponseDto>>> UpdateCategory(
            Guid categoryId, JsonPatchDocument<CategoryPatchUpdateDto> patchDocument
        )
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(
                        ApiResponse<CategoryResponseDto>
                            .FailureResponse(new List<string> { "Invalid data for update" })
                    );
                }
                var categoryPatch = _mapper.Map<JsonPatchDocument<Category>>(patchDocument);

                var categoryEntity = await _unitOfWork.Categories.GetByIdAsync(categoryId);

                if (categoryEntity == null)
                {
                    return NotFound(
                         ApiResponse<CategoryResponseDto>
                            .FailureResponse(new List<string> { "No Category with the specified id" })
                    );
                }

                categoryPatch.ApplyTo(categoryEntity, ModelState);


                var isValid = TryValidateModel(categoryEntity);

                if(!isValid)
                {
                    return BadRequest(
                        ApiResponse<CategoryResponseDto>
                            .FailureResponse(new List<string> { "Invalid model properties" })
                    );
                }

                var entityFromDb = await _unitOfWork.Categories.UpdateAsync(categoryEntity);

                if (entityFromDb == null)
                {
                    return BadRequest(
                        ApiResponse<CategoryResponseDto>
                            .FailureResponse(new List<string> { "Invalid model properties" })
                    );
                }

                var categoryToReturn = _mapper.Map<CategoryResponseDto>(entityFromDb);

                return Ok(
                    ApiResponse<CategoryResponseDto>.SuccessResponse(categoryToReturn)
                );

            } catch(Exception ex)
            {
                return BadRequest(
                    ApiResponse<CategoryResponseDto>
                    .FailureResponse(new List<string> { ex.Message})
                );
            }
        }

        [HttpDelete("{categoryId:Guid}")]
        public async Task<ActionResult> DeleteBook(Guid categoryId)
        {
            try
            {
                var categoryToDelete = await _unitOfWork.Categories.GetByIdAsync(categoryId);

                if (categoryToDelete == null)
                {
                    return NotFound(
                        ApiResponse<BookResponseDto>.FailureResponse(new List<string> { "No category with the Id provided" })
                    );
                }

                var isDeleted = await _unitOfWork.Categories.DeleteAsync(categoryToDelete);

                if (!isDeleted)
                {
                    return NotFound(
                        ApiResponse<CategoryResponseDto>.FailureResponse(new List<string> { "Unable to delete category" })
                    );
                }

                return NoContent();

            }
            catch (Exception ex)
            {
                return BadRequest(
                  ApiResponse<CategoryResponseDto>.FailureResponse(new List<string> { ex.Message })
              );
            }
        }
    }
}
