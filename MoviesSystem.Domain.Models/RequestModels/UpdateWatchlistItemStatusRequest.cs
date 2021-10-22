using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoviesSystem.Domain.Models.RequestModels
{
    public class UpdateWatchlistItemStatusRequest
    {
        public int UserId { get; set; }
        public string MovieId { get; set; }
    }
}
