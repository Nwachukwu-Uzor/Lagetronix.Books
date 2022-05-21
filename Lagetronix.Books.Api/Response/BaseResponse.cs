using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lagetronix.Books.Api.Response
{
    public class BaseResponse
    {
        public bool Success { get; set; }
        public List<string> Errors { get; set; }
    }
}
