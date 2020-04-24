using System;
using System.Collections.Generic;
using System.Text;
using VidRental.DataAccess.DataContext;
using VidRental.DataAccess.DbModels;

namespace VidRental.DataAccess.Repositories
{
    public class MovieRepository : BaseRepository<Movie>, IMovieRepository
    {
        public MovieRepository(VidContext context) : base(context)
        {
        }
    }
}
