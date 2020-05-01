using System;
using System.Collections.Generic;
using System.Text;

namespace VidRental.Services.Dtos.Response.Images
{
    public class FileDeleteResult
    {
        public string Url { get; set; }
        public bool Succeeded { get; set; }
        public string Error { get; set; }

        public static FileDeleteResult Success(string url) 
            => new FileDeleteResult { Error = null, Succeeded = true, Url = url };
        public static FileDeleteResult Failure(string url, string error) 
            => new FileDeleteResult { Error = error, Succeeded = false, Url = url };
    }
}
