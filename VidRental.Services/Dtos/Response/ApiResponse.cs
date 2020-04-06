using System;
using System.Collections.Generic;
using System.Text;

namespace VidRental.Services.Dtos.Response
{
    public class ApiResponse
    {
        public bool IsSuccess { get; set; } = false;
        public IEnumerable<ApiError> Errors { get; set; }

        protected ApiResponse() { }

        public ApiResponse Success() => new ApiResponse { IsSuccess = true };

        public static ApiResponse Failure(IEnumerable<ApiError> errors) => new ApiResponse { Errors = errors };

        public static ApiResponse Failure(params ApiError[] errors) => new ApiResponse { Errors = errors };
    }

    public class ApiResponse<T> : ApiResponse where T : class
    {
        public T Data { get; set; }

        public static ApiResponse<T> Success(T data) => new ApiResponse<T> { Data = data, IsSuccess = true };

        public new static ApiResponse<T> Failure(IEnumerable<ApiError> errors) => new ApiResponse<T> { Errors = errors }; 

        public new static ApiResponse<T> Failure(params ApiError[] errors) => new ApiResponse<T> { Errors = errors }; 
    }
}
