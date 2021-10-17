using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoviesSystem.Domain.Models
{
    public class WatchlistItemGetModel
    {
        public string MovieId { get; set; }
        public bool IsWatched { get; set; }
    }
}
