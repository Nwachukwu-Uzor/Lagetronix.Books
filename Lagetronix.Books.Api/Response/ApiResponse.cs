using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lagetronix.Books.Api.Response
{
    public class ApiResponse<T> : BaseResponse
    {
        public T Data { get; set; }

        public ApiResponse()
        {

        }

        public ApiResponse(T data, bool success = true, List<string> errors = null)
        {
            Success = success;
            Data = data;
            Errors = errors;
        }

        public static ApiResponse<T> SuccessResponse(T data)
        {
            return new ApiResponse<T> { 
                Errors = null,
                Success = true,
                Data = data
            };

        }

        public static ApiResponse<T> FailureResponse(List<string> errors)
        {
            return new ApiResponse<T> {
                Data = default,
                Errors = errors,
                Success = false
            };

        }
    }
}
