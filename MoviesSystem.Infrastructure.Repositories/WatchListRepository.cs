using MoviesSystem.Domain.Repositories;
using MoviesSystem.Infrastructure.Store;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoviesSystem.Infrastructure.Repositories
{
    public class WatchListRepository : IWatchListRepository
    {
        private readonly MoviesDbContext _context;

        public WatchListRepository(MoviesDbContext context)
        {
            _context = context;
        }

        public void GetWatchListItems(int userId)
        {
            var data = _context.WatchList.ToList();
        }
    }
}
