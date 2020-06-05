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
            CreateMap<Cartridge, CartridgeEditDetails>()
                .ForMember(d => d.MovieTitle, o => o.MapFrom(s => s.Movie.Title));
            CreateMap<CartridgeUpdateRequest, Cartridge>();
            CreateMap<Cartridge, CartridgeForRentList>()
                .ForMember(d => d.MovieTitle, o => o.MapFrom(s => s.Movie.Title))
                .ForMember(d => d.MovieDescription, o => o.MapFrom(s => s.Movie.Description));
            CreateMap<Cartridge, CartridgeForRent>()
                .ForMember(d => d.MovieTitle, o => o.MapFrom(s => s.Movie.Title))
                .ForMember(d => d.MovieDescription, o => o.MapFrom(s => s.Movie.Description))
                .ForMember(d => d.MovieDirector, o => o.MapFrom(s => s.Movie.Director))
                .ForMember(d => d.MovieReleaseDate, o => o.MapFrom(s => s.Movie.ReleaseDate))
                .ForMember(d => d.Images, o => o.MapFrom(s => s.Movie.Images));
            CreateMap<Cartridge, CartridgeRental>()
                .ForMember(d => d.MovieTitle, o => o.MapFrom(s => s.Movie.Title))
                .ForMember(d => d.MovieReleaseYear, o => o.MapFrom(s => s.Movie.ReleaseDate.Year));
        }
    }
}
