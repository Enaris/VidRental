using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VidRental.API.AutoMapper
{
    public static class RootProfiles
    {
        public static Type[] Maps => new[]
        {
            typeof(UserProfiles),
            typeof(AddressProfiles),
            typeof(ShopUserProfiles),
            typeof(ShopEmployeeProfiles), 
            typeof(MovieProfiles), 
            typeof(ImageProfiles)
        };
    }
}
