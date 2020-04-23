using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VidRental.DataAccess.DbModels;
using VidRental.Services.Dtos.Request;

namespace VidRental.API.AutoMapper
{
    public class ShopUserProfiles : Profile
    {
        public ShopUserProfiles()
        {
            CreateMap<ShopUserAddRequest, ShopUser>();
        }
    }
}
