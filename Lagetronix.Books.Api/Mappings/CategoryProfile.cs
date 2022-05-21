using AutoMapper;
using Lagetronix.Books.Data.Domain;
using Lagetronix.Books.Data.Dto.Requests.Category;
using Lagetronix.Books.Data.Dto.Responses;

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
                    option => option.MapFrom(src => src.CreatedAt.ToShortDateString())
                )
                .ForMember(
                    dest => dest.ModifiedOn,
                    option => option.MapFrom(src => src.ModifiedOn.ToShortDateString())
                );
        }
    }
}
