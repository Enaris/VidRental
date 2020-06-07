using Microsoft.Extensions.Hosting;
using System.Collections.Generic;
using System.Threading.Tasks;
using VidRental.Services.Dtos.Request.Images;
using VidRental.Services.Dtos.Response.Images;

namespace VidRental.Services.Services
{
    /// <summary>
    /// Contains logic for managing image upload
    /// </summary>
    public class UploadService : IUploadService
    {
        public UploadService()
        {
        }

        /// <summary>
        /// Uploads image
        /// </summary>
        /// <param name="request">Image upload data</param>
        /// <returns>Uploaded image data</returns>
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

        /// <summary>
        /// Uploads images
        /// </summary>
        /// <param name="files">List of images to upload</param>
        /// <returns>List of uploaded images</returns>
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

        /// <summary>
        /// Generates path in server for given filename
        /// </summary>
        /// <param name="filename">Name of the file</param>
        /// <returns>Location of file in server</returns>
        private string GetFilePath(string filename)
        {
            var folderPath = "Static/Images/Movies";
            var fileName = filename;
            return $"{folderPath}/{fileName}";
        }
    }
}
