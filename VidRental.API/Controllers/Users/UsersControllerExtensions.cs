using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VidRental.API.Controllers.Users
{
    public static class UsersControllerExtensions
    {
        public static string NotFoundMessage(this UsersController controller, string id)
            => $"User with id: { id } does not exist";
    }
}
