﻿using System;
using System.Collections.Generic;
using System.Text;

namespace VidRental.Services.Dtos.Response.User
{
    public class UserBaseInfo
    {
        public string Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
    }
}