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

        public GenericResultType<object> Add(int userId, string movieId)
        {
            _watchlistRepository.Insert(userId, movieId);

            return new GenericResultType<object>(HttpStatusCode.NoContent, null);
        }

        public GenericResultType<List<WatchlistItemGetModel>> Get(int userId)
        {
            var data = _watchlistRepository.Get(userId);

            return new GenericResultType<List<WatchlistItemGetModel>>(HttpStatusCode.OK, data);
        }

        public List<UnwatchedMoviesGetModel> GetUnwatchedMovies(int unwatcheMoviesMinCount, int excludedDaysCount)
        {
            return _watchlistRepository.GetUnwatchedMovies(unwatcheMoviesMinCount, excludedDaysCount);
        }

        public GenericResultType<object> MarkAsWatched(int userId, string movieId)
        {
            _watchlistRepository.MarkAsWatched(userId, movieId);

            return new GenericResultType<object>(HttpStatusCode.NoContent, null);
        }
    }
}
