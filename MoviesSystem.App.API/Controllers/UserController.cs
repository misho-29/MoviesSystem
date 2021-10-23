using Microsoft.AspNetCore.Mvc;
using MoviesSystem.Domain.Models.RequestModels;
using MoviesSystem.Domain.Services;
using Swashbuckle.AspNetCore.Annotations;
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
        public IActionResult Post([FromRoute] AddWatchlistItemRequestForUser requestForUser, [FromBody] AddWatchlistItemRequestForMovie requestForMovie)
        {
            var result = _watchlistService.Add(requestForUser.UserId, requestForMovie.MovieId);
            return new ObjectResult(result) { StatusCode = (int)result.StatusCode };
        }

        [HttpGet("{userId}/watchlist")]
        public IActionResult Get([FromRoute] GetUserWatchlistRequest request)
        {
            var result = _watchlistService.Get(request.UserId);
            return new ObjectResult(result) { StatusCode = (int)result.StatusCode };
        }

        [HttpPatch("{userId}/watchlist/{movieId}/watched")]
        public IActionResult Patch([FromRoute] UpdateWatchlistItemStatusRequest request)
        {
            var result = _watchlistService.MarkAsWatched(request.UserId, request.MovieId);
            return new ObjectResult(result) { StatusCode = (int)result.StatusCode };
        }
    }
}
