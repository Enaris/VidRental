using System;
using System.Collections.Generic;
using System.Text;
using VidRental.Services.Dtos.Response.Image;

namespace VidRental.Services.Dtos.Response.Cartridge
{
    public class CartridgeForRent
    {
        public Guid Id { get; set; }
        public string MovieTitle { get; set; }
        public string MovieDescription { get; set; }
        public string Language { get; set; }
        public int Avaible { get; set; }
        public double RentPrice { get; set; }
        public int DaysToReturn { get; set; }
        public string MovieDirector { get; set; }
        public DateTime MovieReleaseDate { get; set; }
        public IEnumerable<MovieImageDto> Images { get; set; }

        public IEnumerable<LanguageLink> OtherLanguages { get; set; }
    }
}
