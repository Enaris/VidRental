using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VidRental.DataAccess.DbModels;
using VidRental.Services.Dtos.Response.Rental;

namespace VidRental.API.AutoMapper
{
    public class RentalProfiles : Profile
    {
        public RentalProfiles()
        {
            CreateMap<Rental, RentalBaseInfo>();
            CreateMap<Rental, RentalForAdminList>();
        }
    }
}
