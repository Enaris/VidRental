using System;
using System.Collections.Generic;
using System.Text;
using VidRental.Services.Dtos.Response.User;

namespace VidRental.Services.Dtos.Auth
{
    public class RefreshTokenResult
    {
        public string Token { get; set; }
        public UserBaseInfo User { get; set; }
    }
}
