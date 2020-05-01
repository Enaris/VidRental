using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VidRental.DataAccess.DbModels;
using VidRental.Services.Dtos.Response.Image;

namespace VidRental.API.AutoMapper
{
    public class ImageProfiles : Profile
    {
        public ImageProfiles()
        {
            CreateMap<Image, ImageDto>()
                .ReverseMap();
            CreateMap<MovieImage, MovieImageDto>()
                .ForMember(d => d.Url, o => o.MapFrom(s => s.Image.Url));
        }
    }
}
