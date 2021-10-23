using AutoMapper;
using MoviesSystem.Domain.Models;
using MoviesSystem.Domain.Repositories;
using MoviesSystem.Infrastructure.Store;
using Db = MoviesSystem.Infrastructure.Store.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MoviesSystem.Domain.Models.Responses;

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

        public List<WatchlistItemResponse> Get(int userId)
        {
            var watchlistItems = _context.Watchlist.Where(item => item.UserId == userId);

            return _mapper.Map<List<WatchlistItemResponse>>(watchlistItems);

        }

        public void Insert(int userId, string movieId)
        {
            var record = new Db.WatchListItem
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

            _context.SaveChanges();
        }

        public List<UnwatchedMoviesResponse> GetUnwatchedMovies(int unwatcheMoviesMinCount, int excludedDaysCount)
        {
            var userIds = _context.Watchlist
                .Where(item => item.IsWatched == false)
                .GroupBy(item => item.UserId)
                .Where(grouped => grouped.Count() > unwatcheMoviesMinCount)
                .Select(grouped => grouped.Key)
                .ToList();

            return userIds.Select(userId => new UnwatchedMoviesResponse
            {
                UserId = userId,
                Movies = _context.Watchlist.Where(item => item.UserId == userId && item.IsWatched == false
                    && (item.LastNotificationDateTime == null || item.LastNotificationDateTime < DateTime.Now.AddDays(-excludedDaysCount)))
                    .Select(item => new MovieModel
                    {
                        MovieId = item.MovieId,
                        LastNotificationDateTime = item.LastNotificationDateTime,
                    }).ToList(),
            }).ToList();
        }
    }
}
