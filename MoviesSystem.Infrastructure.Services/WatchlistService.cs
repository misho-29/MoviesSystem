using MoviesSystem.Domain.Models;
using MoviesSystem.Domain.Models.Responses;
using MoviesSystem.Domain.Repositories;
using MoviesSystem.Domain.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace MoviesSystem.Infrastructure.Services
{
    public class WatchlistService : IWatchlistService
    {
        private readonly IWatchlistRepository _watchlistRepository;

        public WatchlistService(IWatchlistRepository watchlistRepository)
        {
            _watchlistRepository = watchlistRepository;
        }

        public GenericResult<object> Add(int userId, string movieId)
        {
            _watchlistRepository.Insert(userId, movieId);

            return new GenericResult<object>(HttpStatusCode.NoContent, null);
        }

        public GenericResult<List<WatchlistItemResponse>> Get(int userId)
        {
            var data = _watchlistRepository.Get(userId);

            return new GenericResult<List<WatchlistItemResponse>>(HttpStatusCode.OK, data);
        }

        public List<UnwatchedMoviesResponse> GetUnwatchedMovies(int unwatcheMoviesMinCount, int excludedDaysCount)
        {
            return _watchlistRepository.GetUnwatchedMovies(unwatcheMoviesMinCount, excludedDaysCount);
        }

        public GenericResult<object> MarkAsWatched(int userId, string movieId)
        {
            _watchlistRepository.MarkAsWatched(userId, movieId);

            return new GenericResult<object>(HttpStatusCode.NoContent, null);
        }
    }
}
