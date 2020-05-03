using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VidRental.DataAccess.DbModels;
using VidRental.Services.Dtos.Request;
using VidRental.Services.Dtos.Response.Cartridge;

namespace VidRental.API.AutoMapper
{
    public class CartridgeProfiles : Profile
    {
        public CartridgeProfiles()
        {
            CreateMap<CartridgeAddRequest, Cartridge>();
            CreateMap<Cartridge, CartridgeForList>()
                .ForMember(d => d.MovieTitle, o => o.MapFrom(s => s.Movie.Title));
            CreateMap<Cartridge, CartridgeDetails>()
                .ForMember(d => d.MovieTitle, o => o.MapFrom(s => s.Movie.Title));
            CreateMap<CartridgeUpdateRequest, Cartridge>();
        }
    }
}
