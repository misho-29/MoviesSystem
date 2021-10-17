using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoviesSystem.Infrastructure.Store.Models
{
    public class WatchListItem
    {
        public int UserId { get; set; }
        public string MovieId { get; set; }
        public bool IsWatched { get; set; }
        public DateTime LastNotificationDateTime { get; set; }
    }
}
