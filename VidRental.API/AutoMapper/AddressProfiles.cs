﻿using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VidRental.DataAccess.DbModels;
using VidRental.Services.Dtos.Request;
using VidRental.Services.Dtos.Response.Address;

namespace VidRental.API.AutoMapper
{
    public class AddressProfiles : Profile
    {
        public AddressProfiles()
        {
            CreateMap<AddressAddRequest, Address>();
            CreateMap<Address, AddressDto>();
        }
    }
}
