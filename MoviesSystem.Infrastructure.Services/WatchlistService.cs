using MoviesSystem.Domain.Models;
using MoviesSystem.Domain.Repositories;
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
        private readonly IWatchlistRepository _watchlistRepository;

        public WatchlistService(IWatchlistRepository watchlistRepository)
        {
            _watchlistRepository = watchlistRepository;
        }

        public void Add(int userId, string movieId)
        {
            _watchlistRepository.Insert(userId, movieId);
        }

        public List<WatchlistItemGetModel> Get(int userId)
        {
            return _watchlistRepository.Get(userId);
        }

        public void MarkAsWatched(int userId, string movieId)
        {
            _watchlistRepository.MarkAsWatched(userId, movieId);
        }
    }
}
