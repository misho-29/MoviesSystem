using MoviesSystem.Domain.Models;
using MoviesSystem.Domain.Models.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoviesSystem.Domain.Services
{
    public interface IWatchlistService
    {
        GenericResult<object> Add(int userId, string movieId);
        GenericResult<List<WatchlistItemResponse>> Get(int userId);
        GenericResult<object> MarkAsWatched(int userId, string movieId);
        List<UnwatchedMoviesResponse> GetUnwatchedMovies(int unwatcheMoviesMinCount, int excludedDaysCount);
    }
}
