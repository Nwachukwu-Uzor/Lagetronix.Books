using AutoMapper;
using Lagetronix.Books.Data.Domain;
using Lagetronix.Books.Data.Dto.Requests.Category;
using Lagetronix.Books.Data.Dto.Responses;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.JsonPatch.Operations;
using System;

namespace Lagetronix.Books.Api.Mappings
{
    public class CategoryProfile : Profile
    {
        public CategoryProfile()
        {
            CreateMap<CategoryRegistrationDto, Category>();

            CreateMap<Category, CategoryResponseDto>()
                .ForMember(
                    dest => dest.CreatedAt, 
                    option => option.MapFrom(src => src.CreatedAt.ToLongDateString())
                )
                .ForMember(
                    dest => dest.ModifiedOn,
                    option => option.MapFrom(src => src.ModifiedOn.ToLongDateString())
                );

            CreateMap<CategoryPatchUpdateDto, Category>()
                .ForMember(
                    dest => dest.ModifiedOn,
                    option => option.MapFrom(src => DateTime.UtcNow)
                )
                .ForMember(
                    dest => dest.Id,
                    option => option.UseDestinationValue()
                );

            CreateMap<JsonPatchDocument<CategoryPatchUpdateDto>, JsonPatchDocument<Category>>();
            CreateMap<Operation<CategoryPatchUpdateDto>, Operation<Category>>();
        }
    }
}
