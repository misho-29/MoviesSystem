using Microsoft.AspNetCore.Mvc;
using MoviesSystem.Domain.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace MoviesSystem.App.API.Controllers
{
    [ApiController]
    [Route("api/[controller]s")]
    public class UserController : ControllerBase
    {
        private readonly IWatchlistService _watchlistService;

        public UserController(IWatchlistService watchlistService)
        {
            _watchlistService = watchlistService;
        }

        [HttpPost("{userId}/watchlist")]
        public IActionResult Post(int userId, [FromBody] string movieId)
        {
            _watchlistService.Add(userId, movieId);
            return NoContent();
        }

        [HttpGet("{userId}/watchlist")]
        public IActionResult Get(int userId)
        {
            var userWatchlist = _watchlistService.Get(userId);

            return Ok(userWatchlist);
        }

        [HttpPatch("{userId}/watchlist/{movieId}/watched")]
        public IActionResult Patch(int userId, string movieId)
        {
            _watchlistService.MarkAsWatched(userId, movieId);

            return NoContent();
        }
    }
}
