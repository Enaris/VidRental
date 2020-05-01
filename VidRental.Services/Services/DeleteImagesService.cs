using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using VidRental.Services.Dtos.Response.Images;

namespace VidRental.Services.Services
{
    public class DeleteImagesService : IDeleteImagesService
    {
        public DeleteImagesService()
        {

        }

        public FileDeleteResult DeleteImage(string url)
        {
            if (!File.Exists(url))
                return FileDeleteResult.Failure(url, "File does exists");

            try
            {
                File.Delete(url);
            }
            catch (Exception e)
            {
                return FileDeleteResult.Failure(url, e.Message);
            }

            return FileDeleteResult.Success(url);
        }

        public IEnumerable<FileDeleteResult> DeleteImages(IEnumerable<string> urls)
        {
            var result = new List<FileDeleteResult>();
            foreach (var u in urls)
            {
                var deleteResult = DeleteImage(u);
                result.Add(deleteResult);
            }

            return result;
        }


    }
}
