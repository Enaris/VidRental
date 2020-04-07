using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace VidRental.Services.ResponseWrapper
{
    public class ApiResponseErrors : Dictionary<string, IEnumerable<string>>
    {
        public ApiResponseErrors(IEnumerable<KeyValuePair<string, IEnumerable<string>>> value) : base(value) { }

        public ApiResponseErrors(string key, string value) 
            : base (new List<KeyValuePair<string, IEnumerable<string>>> { new KeyValuePair<string, IEnumerable<string>> (key, new List<string> { value })}) {  }

        public ApiResponseErrors(string key, IEnumerable<string> values)
            : base(new List<KeyValuePair<string, IEnumerable<string>>> { new KeyValuePair<string, IEnumerable<string>>(key, values )}) { }
    }
}
