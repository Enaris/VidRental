using System;
using System.Collections.Generic;
using System.Text;

namespace VidRental.Services.Dtos.Response.Cartridge
{
    public class LanguageLink
    {
        public Guid CartridgeId { get; set; }
        public string Language { get; set; }
    }
}
