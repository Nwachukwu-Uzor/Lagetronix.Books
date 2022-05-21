using AutoMapper;
using Lagetronix.Books.Api.Response;
using Lagetronix.Books.Data.Contracts;
using Lagetronix.Books.Data.Domain;
using Lagetronix.Books.Data.Dto.Requests.Book;
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
    public class BooksController : BaseController
    {
        public BooksController(IUnitOfWork unitOfWork, IMapper mapper)
        : base(unitOfWork, mapper)
        {

        }

        [HttpPost]
        public async Task<ActionResult<ApiResponse<BookResponseDto>>> CreateBook(BookRegistrationDto bookRegistrationDto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(
                        ApiResponse<BookResponseDto>.FailureResponse(
                            new List<string> { "Unable to create a book with the data provided" }
                        )
                    );
                }

                var book = _mapper.Map<Book>(bookRegistrationDto);

                var category = await _unitOfWork.Categories.GetByIdAsync(bookRegistrationDto.CategoryId);

                if (category == null)
                {
                    return NotFound(
                        ApiResponse<Book>.FailureResponse(new List<string> { "No Category with the Id provided" })
                    );
                }

                book.Category = category;

                var bookEntity = await _unitOfWork.Books.AddAsync(book);

                if (bookEntity == null)
                {
                    return BadRequest(
                        ApiResponse<BookResponseDto>.FailureResponse(new List<string> { "Unable to create book" })
                    );
                }

                var bookToReturn = _mapper.Map<BookResponseDto>(bookEntity);

                return CreatedAtRoute(
                    nameof(GetBookById),
                    new { bookId = bookEntity.Id },
                    ApiResponse<BookResponseDto>.SuccessResponse(bookToReturn)
                );

            }
            catch (Exception ex)
            {
                return BadRequest(
                    ApiResponse<BookResponseDto>.FailureResponse(new List<string> { ex.Message })
                );
            }
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<BookResponseDto>>> GetAllBooks(int page = 1, int size = 20, bool includeCategory = false)
        {
            try
            {
                var books = await _unitOfWork.Books.GetAllBooksAsync(page, size, includeCategory);

                var booksToReturn = _mapper.Map<IEnumerable<BookResponseDto>>(books);

                Response.Headers.Add("page", page.ToString());

                return Ok(
                    ApiResponse<IEnumerable<BookResponseDto>>.SuccessResponse(booksToReturn)
                );
            }
            catch (Exception ex)
            {
                return BadRequest(
                    ApiResponse<BookResponseDto>.FailureResponse(new List<string> { ex.Message })
                );
            }
        }


        [HttpGet("favorite")]
        public async Task<ActionResult<IEnumerable<BookResponseDto>>> GetFavoriteBooks(int page = 1, int size = 20, bool includeCategory = false)
        {
            try
            {
                var books = await _unitOfWork.Books.GetFavoriteBooks(page, size, includeCategory);

                var booksToReturn = _mapper.Map<IEnumerable<BookResponseDto>>(books);

                Response.Headers.Add("page", page.ToString());

                return Ok(
                    ApiResponse<IEnumerable<BookResponseDto>>.SuccessResponse(booksToReturn)
                );
            }
            catch (Exception ex)
            {
                return BadRequest(
                    ApiResponse<BookResponseDto>.FailureResponse(new List<string> { ex.Message })
                );
            }
        }

        [HttpGet("{bookId:Guid}", Name = nameof(GetBookById))]
        public async Task<ActionResult<BookResponseDto>> GetBookById(Guid bookId)
        {
            try
            {
                var book = await _unitOfWork.Books.GetByIdAsync(bookId);

                if (book == null)
                {
                    return NotFound(
                        ApiResponse<BookResponseDto>.FailureResponse(new List<string> { "No book with the Id provided" })
                    );
                }

                var bookToReturn = _mapper.Map<BookResponseDto>(book);

                return Ok(
                    ApiResponse<BookResponseDto>.SuccessResponse(bookToReturn)
                );

            }
            catch (Exception ex)
            {
                return BadRequest(
                    ApiResponse<BookResponseDto>.FailureResponse(new List<string> { ex.Message })
                );
            }
        }

        [HttpPut("{bookId:Guid}")]
        public async Task<ActionResult<ApiResponse<BookResponseDto>>> UpdateBook(Guid bookId, BookPutUpdateDto bookUpdateDto)
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

                var bookEntity = await _unitOfWork.Books.GetByIdAsync(bookId);

                if (bookEntity == null)
                {
                    return NotFound(
                         ApiResponse<BookResponseDto>
                            .FailureResponse(new List<string> { "No book with the specified id" })
                    );
                }

                var bookUpdateEntity = _mapper.Map(bookUpdateDto, bookEntity);

                var updatedBook = await _unitOfWork.Books.UpdateAsync(bookUpdateEntity);

                if (updatedBook == null)
                {
                    return BadRequest(
                        ApiResponse<BookResponseDto>
                            .FailureResponse(new List<string> { "Invalid model properties" })
                    );
                }

                var bookToReturn = _mapper.Map<BookResponseDto>(updatedBook);

                return Ok(
                    ApiResponse<BookResponseDto>.SuccessResponse(bookToReturn)
                );

            } catch(Exception ex)
            {
                return BadRequest(
                   ApiResponse<BookResponseDto>
                   .FailureResponse(new List<string> { ex.Message })
               );
            }
        }

        [HttpPatch("{bookId:Guid}")]
        public async Task<ActionResult<ApiResponse<BookResponseDto>>> UpdateBook(
            Guid bookId, JsonPatchDocument<BookPatchUpdateDto> patchDocument
        )
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
                var bookPatch = _mapper.Map<JsonPatchDocument<Book>>(patchDocument);

                var bookEntity = await _unitOfWork.Books.GetByIdAsync(bookId);

                if (bookEntity == null)
                {
                    return NotFound(
                         ApiResponse<BookResponseDto>
                            .FailureResponse(new List<string> { "No book with the specified id" })
                    );
                }

                bookPatch.ApplyTo(bookEntity, ModelState);


                var isValid = TryValidateModel(bookEntity);

                if (!isValid)
                {
                    return BadRequest(
                        ApiResponse<BookResponseDto>
                            .FailureResponse(new List<string> { "Invalid model properties" })
                    );
                }

                var entityFromDb = await _unitOfWork.Books.UpdateAsync(bookEntity);

                if (entityFromDb == null)
                {
                    return BadRequest(
                        ApiResponse<BookResponseDto>
                            .FailureResponse(new List<string> { "Invalid model properties" })
                    );
                }

                var bookToReturn = _mapper.Map<BookResponseDto>(entityFromDb);

                return Ok(
                    ApiResponse<BookResponseDto>.SuccessResponse(bookToReturn)
                );

            }
            catch (Exception ex)
            {
                return BadRequest(
                    ApiResponse<BookResponseDto>
                    .FailureResponse(new List<string> { ex.Message })
                );
            }
        }


        [HttpPut("favorite/{bookId:Guid}")]
        public async Task<ActionResult<ApiResponse<BookResponseDto>>> SetFavoritesStatus(Guid bookId)
        {
            try
            {
                var book = await _unitOfWork.Books.GetByIdAsync(bookId);

                if (book == null)
                {
                    return NotFound(
                        ApiResponse<BookResponseDto>.FailureResponse(new List<string> { "No book with the Id provided" })
                    );
                }

                book.IsFavorite = !book.IsFavorite;

                var bookEntity = await _unitOfWork.Books.UpdateAsync(book);

                var bookToReturn = _mapper.Map<BookResponseDto>(bookEntity);

                return Ok(
                    ApiResponse<BookResponseDto>.SuccessResponse(bookToReturn)
                );

            } catch(Exception ex)
            {
                return BadRequest(
                   ApiResponse<BookResponseDto>.FailureResponse(new List<string> { ex.Message })
               );
            }
        }

        [HttpPut("{bookId:Guid}/updateCategory")]
        public async Task<ActionResult<ApiResponse<BookResponseDto>>> UpdateBookCategory(Guid bookId, BookCategoryUpdateDto updateDto)
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

                var bookToUpdate = await _unitOfWork.Books.GetByIdAsync(bookId);

                if (bookToUpdate == null)
                {
                    return NotFound(
                        ApiResponse<BookResponseDto>
                            .FailureResponse(new List<string> { "Invalid book Id" })
                    );
                }

                var category = await _unitOfWork.Categories.GetByIdAsync(updateDto.CategoryId);

                if (category == null)
                {
                    return NotFound(
                        ApiResponse<BookResponseDto>
                            .FailureResponse(new List<string> { "Invalid category Id" })
                    );
                }

                bookToUpdate.Category = category;

                var updatedBookEntity = await _unitOfWork.Books.UpdateAsync(bookToUpdate);

                if (updatedBookEntity == null)
                {
                    return BadRequest(
                        ApiResponse<BookResponseDto>
                            .FailureResponse(new List<string> { "Unable to update book's category" })
                    );
                }


                var bookToReturn = _mapper.Map<BookResponseDto>(updatedBookEntity);

                return Ok(
                    ApiResponse<BookResponseDto>.SuccessResponse(bookToReturn)
                );
            } catch(Exception ex)
            {
                return BadRequest(
                  ApiResponse<BookResponseDto>.FailureResponse(new List<string> { ex.Message })
              );
            }
        }

        [HttpDelete("{bookId:Guid}")]
        public async Task<ActionResult> DeleteBook(Guid bookId)
        {
            try
            {
                var bookToDelete = await _unitOfWork.Books.GetByIdAsync(bookId);

                if (bookToDelete == null)
                {
                    return NotFound(
                        ApiResponse<BookResponseDto>.FailureResponse(new List<string> { "No book with the Id provided" })
                    );
                }

                var isDeleted = await _unitOfWork.Books.DeleteAsync(bookToDelete);

                if (!isDeleted)
                {
                    return NotFound(
                        ApiResponse<BookResponseDto>.FailureResponse(new List<string> { "Unable to delete book" })
                    );
                }

                return NoContent();

            } catch(Exception ex)
            {
                return BadRequest(
                  ApiResponse<BookResponseDto>.FailureResponse(new List<string> { ex.Message })
              );
            }
        }
    }
}
