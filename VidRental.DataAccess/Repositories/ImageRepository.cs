using System;
using System.Collections.Generic;
using System.Text;
using VidRental.DataAccess.DataContext;
using VidRental.DataAccess.DbModels;

namespace VidRental.DataAccess.Repositories
{
    public class ImageRepository : BaseRepository<Image>, IImageRepository
    {
        public ImageRepository(VidContext context) : base(context)
        {
        }
    }
}
