using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VidRental.Services.ResponseWrapper;

namespace VidRental.API.ActionFilters
{
    public class ValidateModelStateAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (!context.ModelState.IsValid)
            {
                var errors = context.ModelState
                    .Select(e => new KeyValuePair<string, IEnumerable<string>>(e.Key, e.Value.Errors.Select(er => er.ErrorMessage)));
                var apiResponse = ApiResponse.Failure(new ApiResponseErrors(errors));

                context.Result = new JsonResult(apiResponse)
                {
                    StatusCode = 400
                };
            }
        }
    }
}
