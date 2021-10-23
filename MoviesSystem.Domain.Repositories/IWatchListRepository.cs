using MoviesSystem.Domain.Models;
using MoviesSystem.Domain.Models.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoviesSystem.Domain.Repositories
{
    public interface IWatchlistRepository
    {
        void Insert(int userId, string movieId);
        List<WatchlistItemResponse> Get(int userId);
        void MarkAsWatched(int userId, string movieId);
        List<UnwatchedMoviesResponse> GetUnwatchedMovies(int unwatcheMoviesMinCount, int excludedDaysCount);
    }
}
