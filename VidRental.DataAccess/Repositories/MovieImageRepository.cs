using System;
using System.Collections.Generic;
using System.Text;
using VidRental.DataAccess.DataContext;
using VidRental.DataAccess.DbModels;

namespace VidRental.DataAccess.Repositories
{
    public class MovieImageRepository : BaseRepository<MovieImage>, IMovieImageRepository
    {
        public MovieImageRepository(VidContext context) : base(context)
        {
        }
    }
}
