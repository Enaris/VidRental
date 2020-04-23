using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VidRental.DataAccess.DbModels;
using VidRental.Services.Dtos.Request;
using VidRental.Services.Dtos.Response.Employee;

namespace VidRental.API.AutoMapper
{
    public class ShopEmployeeProfiles : Profile
    {
        public ShopEmployeeProfiles()
        {
            CreateMap<EmployeeAddRequest, ShopEmployee>();
            CreateMap<User, EmployeeForListFlat>();
            CreateMap<ShopEmployee, EmployeeForListFlat>()
                .IncludeMembers(s => s.User)
                .ForMember(d => d.EmployeeId, o => o.MapFrom(s => s.Id));
            CreateMap<EmployeeAddRequestFlat, User>();
        }
    }
}
