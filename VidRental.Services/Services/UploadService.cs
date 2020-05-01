using Microsoft.Extensions.Hosting;
using System.Collections.Generic;
using System.Threading.Tasks;
using VidRental.Services.Dtos.Request.Images;
using VidRental.Services.Dtos.Response.Images;

namespace VidRental.Services.Services
{
    public class UploadService : IUploadService
    {
        public UploadService()
        {
        }

        public async Task<UploadedImage> UploadImage(ImageUpRequest request)
        {
            if (request.Image.Length <= 0)
            {
                return null;
            }

            var filePath = GetFilePath(request.Image.FileName);

            using (var stream = System.IO.File.Create(filePath))
            {
                await request.Image.CopyToAsync(stream);
            }

            return new UploadedImage { Filepath = filePath, ImageType = request.ImageType };
        }

        public async Task<IEnumerable<UploadedImage>> UploadImages(IEnumerable<ImageUpRequest> files)
        {
            var result = new List<UploadedImage>();
            foreach (var f in files)
            {
                var uploadedImage = await UploadImage(f);
                if (uploadedImage == null)
                    continue;

                result.Add(uploadedImage);
            }

            return result;
        }

        private string GetFilePath(string filename)
        {
            var folderPath = "Static/Images/Movies";
            var fileName = filename;
            return $"{folderPath}/{fileName}";
        }
    }
}
