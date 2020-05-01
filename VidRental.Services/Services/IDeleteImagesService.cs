using System.Collections.Generic;
using VidRental.Services.Dtos.Response.Images;

namespace VidRental.Services.Services
{
    public interface IDeleteImagesService
    {
        FileDeleteResult DeleteImage(string url);
        IEnumerable<FileDeleteResult> DeleteImages(IEnumerable<string> urls);
    }
}