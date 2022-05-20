using AutoMapper;
using Lagetronix.Books.Data.Domain;
using Lagetronix.Books.Data.Dto.Requests;
using Lagetronix.Books.Data.Dto.Responses;

namespace Lagetronix.Books.Api.Mappings
{
    public class CategoryProfile : Profile
    {
        public CategoryProfile()
        {
            CreateMap<Category, CategoryRegistrationDto>();
            CreateMap<Category, CategoryResponseDto>();
        }
    }
}
