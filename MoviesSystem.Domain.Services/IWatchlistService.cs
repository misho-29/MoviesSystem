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
        GenericResultType<object> Add(int userId, string movieId);
        GenericResultType<List<WatchlistItemGetModel>> Get(int userId);
        GenericResultType<object> MarkAsWatched(int userId, string movieId);
        List<UnwatchedMoviesGetModel> GetUnwatchedMovies(int unwatcheMoviesMinCount, int excludedDaysCount);
    }
}
