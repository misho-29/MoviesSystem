using AutoMapper;
using MoviesSystem.Domain.Models;
using MoviesSystem.Domain.Repositories;
using MoviesSystem.Infrastructure.Store;
using MoviesSystem.Infrastructure.Store.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoviesSystem.Infrastructure.Repositories
{
    public class WatchlistRepository : IWatchlistRepository
    {
        private readonly MoviesDbContext _context;
        private readonly IMapper _mapper;

        public WatchlistRepository(MoviesDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public List<WatchlistItemGetModel> Get(int userId)
        {
            var watchlistItems = _context.Watchlist.Where(item => item.UserId == userId);

            return _mapper.Map<List<WatchlistItemGetModel>>(watchlistItems);
            
        }

        public void Insert(int userId, string movieId)
        {
            var record = new WatchListItem
            {
                UserId = userId,
                MovieId = movieId,
            };

            _context.Add(record);
            _context.SaveChanges();
        }

        public void MarkAsWatched(int userId, string movieId)
        {
            _context.Watchlist.First(item => item.UserId == userId && item.MovieId == movieId)
                .IsWatched = true;
        }
    }
}
