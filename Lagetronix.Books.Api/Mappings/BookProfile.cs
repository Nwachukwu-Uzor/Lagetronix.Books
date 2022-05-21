using AutoMapper;
using Lagetronix.Books.Data.Domain;
using Lagetronix.Books.Data.Dto.Requests.Book;
using Lagetronix.Books.Data.Dto.Responses;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.JsonPatch.Operations;
using System;

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

            CreateMap<BookPutUpdateDto, Book>()
                .ForMember(dest => dest.Category, opt => opt.UseDestinationValue())
                .ForMember(dest => dest.CategoryId, opt => opt.UseDestinationValue())
                .ForMember(dest => dest.CreatedAt, opt => opt.UseDestinationValue())
                .ForMember(dest => dest.IsFavorite, opt => opt.UseDestinationValue())
                .ForMember(dest => dest.ModifiedOn, opt => opt.MapFrom(src => DateTime.UtcNow));

            CreateMap<BookPatchUpdateDto, Book>()
               .ForMember(
                   dest => dest.ModifiedOn,
                   option => option.MapFrom(src => DateTime.UtcNow)
               )
               .ForMember(
                   dest => dest.Id,
                   option => option.UseDestinationValue()
               );

            CreateMap<JsonPatchDocument<BookPatchUpdateDto>, JsonPatchDocument<Book>>();
            CreateMap<Operation<BookPatchUpdateDto>, Operation<Book>>();
        }
    }
}
