using System;
using System.Collections.Generic;
using System.Text;

namespace VidRental.Services.ResponseWrapper
{
    public class ApiResponse
    {
        public bool Succeeded { get; set; }

        public ApiResponseErrors Errors { get; set; }

        public static ApiResponse Failure(ApiResponseErrors errors)
            => new ApiResponse { Succeeded = false, Errors = errors };

        public static ApiResponse Failure(string key, string value)
            => new ApiResponse { Succeeded = false, Errors = new ApiResponseErrors(key, value) };
        public static ApiResponse Failure(string key, IEnumerable<string> values)
            => new ApiResponse { Succeeded = false, Errors = new ApiResponseErrors(key, values) };

        public static ApiResponse Success()
            => new ApiResponse { Succeeded = true };
    }

    public class ApiResponse<T> : ApiResponse where T : class
    {
        public T Data { get; set; }

        public new static ApiResponse<T> Failure(ApiResponseErrors errors)
            => new ApiResponse<T> { Succeeded = false, Errors = errors };

        public new static ApiResponse<T> Failure(string key, string value)
            => new ApiResponse<T> { Succeeded = false, Errors = new ApiResponseErrors(key, value) };
        public new static ApiResponse<T> Failure(string key, IEnumerable<string> values)
            => new ApiResponse<T> { Succeeded = false, Errors = new ApiResponseErrors(key, values) };

        public static ApiResponse<T> Success(T data)
            => new ApiResponse<T> { Succeeded = true, Data = data };
    }

}
