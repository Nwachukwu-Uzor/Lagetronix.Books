using AutoMapper;
using Lagetronix.Books.Data.Domain;
using Lagetronix.Books.Data.Dto.Requests;

namespace Lagetronix.Books.Api.Mappings
{
    public class CategoryProfile : Profile
    {
        public CategoryProfile()
        {
            CreateMap<Category, CategoryRegistrationDto>();
        }
    }
}
