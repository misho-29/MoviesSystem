using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoviesSystem.Domain.Models.Responses
{
    public class UnwatchedMoviesResponse
    {
        public int UserId { get; set; }
        public List<MovieModel> Movies { get; set; }
    }

    public class MovieModel
    {
        public string MovieId { get; set; }
        public DateTime? LastNotificationDateTime { get; set; }
    }
}
