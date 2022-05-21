using AutoMapper;
using Lagetronix.Books.Data.Domain;
using Lagetronix.Books.Data.Dto.Requests.Book;
using Lagetronix.Books.Data.Dto.Responses;

namespace Lagetronix.Books.Api.Mappings
{
    public class BookProfile : Profile
    {
        public BookProfile()
        {
            CreateMap<BookRegistrationDto, Book>();
            CreateMap<Book, BookResponseDto>()
                .ForMember(
                    dest => dest.Category,
                    opt => opt.MapFrom(src => src.Category.Name)
                )
                .ForMember(
                    dest => dest.CreatedAt,
                    option => option.MapFrom(src => src.CreatedAt.ToShortDateString())
                )
                .ForMember(
                    dest => dest.ModifiedOn,
                    option => option.MapFrom(src => src.ModifiedOn.ToShortDateString())
                ); 
        }
    }
}
