using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VidRental.DataAccess.DbModels;
using VidRental.Services.Dtos.Request;
using VidRental.Services.Dtos.Response.User;

namespace VidRental.API.AutoMapper
{
    public class UserProfiles : Profile
    {
        public UserProfiles()
        {
            CreateMap<User, UserBaseInfo>();
            CreateMap<RegisterRequest, User>();
        }
    }
}
