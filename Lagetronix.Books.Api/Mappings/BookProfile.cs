using AutoMapper;
using Lagetronix.Books.Data.Domain;
using Lagetronix.Books.Data.Dto.Requests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lagetronix.Books.Api.Mappings
{
    public class BookProfile : Profile
    {
        public BookProfile()
        {
            CreateMap<Book, BookRegistrationDto>();
        }
    }
}
