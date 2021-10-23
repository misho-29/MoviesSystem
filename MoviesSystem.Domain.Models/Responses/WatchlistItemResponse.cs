using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoviesSystem.Domain.Models.Responses
{
    public class WatchlistItemResponse
    {
        public string MovieId { get; set; }
        public bool IsWatched { get; set; }
    }
}
