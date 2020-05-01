using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VidRental.DataAccess.DbModels;
using VidRental.Services.Dtos.Request;
using VidRental.Services.Dtos.Response.Movie;

namespace VidRental.API.AutoMapper
{
    public class MovieProfiles : Profile
    {
        public MovieProfiles()
        {
            CreateMap<MovieAddRequest, Movie>()
                .ForMember(s => s.Images, o => o.Ignore());
            CreateMap<Movie, MovieDetails>();
            CreateMap<Movie, MovieForList>();
            CreateMap<MovieUpdateRequest, Movie>();
        }
    }
}
