using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VidRental.API.Controllers.Auth
{
    public static class AuthControllerExtensions
    {
        public static string UserExistsMessage(this AuthController controller, string email)
            => $"'Email' Account with email: '{ email }' already exists";
        public static string BadLoginMessage(this AuthController controller)
            => "Login or password is invalid";
    }
}
