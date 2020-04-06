using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VidRental.Services.Dtos.Response.User;

namespace VidRental.Services.Dtos.Auth
{
    public class RegisterResult
    {
        public UserBaseInfo NewUser { get; set; }
        public IdentityResult IdentityResult { get; set; }

        public bool Succeeded => IdentityResult.Succeeded;
        public IEnumerable<string> Errros => IdentityResult.Errors.Select(iError => iError.Description);
    }
}
