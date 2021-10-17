using MoviesSystem.Domain.Models;
using MoviesSystem.Domain.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoviesSystem.Infrastructure.Services
{
    public class WatchlistService : IWatchlistService
    {
        public void Add(int userId, string movieId)
        {
            throw new NotImplementedException();
        }

        public List<WatchlistItemGetModel> Get(int userId)
        {
            throw new NotImplementedException();
        }

        public void MarkAsWatched(int userId, string movieId)
        {
            throw new NotImplementedException();
        }
    }
}
