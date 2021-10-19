using MoviesSystem.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoviesSystem.Domain.Services
{
    public interface IWatchlistService
    {
        void Add(int userId, string movieId);
        List<WatchlistItemGetModel> Get(int userId);
        void MarkAsWatched(int userId, string movieId);
        List<UnwatchedMoviesGetModel> GetUnwatchedMovies(int unwatcheMoviesMinCount, int excludedDaysCount);
    }
}
