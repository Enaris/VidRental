using System;
using System.Collections.Generic;
using System.Text;

namespace VidRental.Services.Dtos.Response
{
    public class ApiError
    {
        public string Field { get; set; }

        public IEnumerable<string> Errors { get; set; }

        public ApiError() { }
        public ApiError(string error) { Errors = new[] { error }; }
        public ApiError (string field, string error) 
            : this(error) { Field = field; }
        public ApiError(IEnumerable<string> errors) { Errors = errors; }
        public ApiError(string field, IEnumerable<string> errors)
            : this(errors) { Field = field; }
    }
}
