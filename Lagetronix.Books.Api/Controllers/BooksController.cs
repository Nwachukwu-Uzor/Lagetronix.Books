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
    }
}
