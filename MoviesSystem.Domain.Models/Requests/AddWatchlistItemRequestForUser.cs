using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoviesSystem.Domain.Models.Requests
{
    public class AddWatchlistItemRequestForUser
    {
        public int UserId { get; set; }
    }
}
