using Microsoft.AspNetCore.Mvc;
using MoviesSystem.Domain.Models;
using MoviesSystem.Domain.Models.Requests;
using MoviesSystem.Domain.Models.Responses;
using MoviesSystem.Domain.Services;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace MoviesSystem.App.API.Controllers
{
    [Produces("application/json")]
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly IWatchlistService _watchlistService;


        public UsersController(IWatchlistService watchlistService)
        {
            _watchlistService = watchlistService;
        }

        /// <summary>
        /// Adds movie to watchlist
        /// </summary>
        [ProducesResponseType(204)]
        [ProducesResponseType(typeof(GenericResult<object>), 400)]
        [HttpPost("{userId}/watchlist")]
        public IActionResult Post([FromRoute] AddWatchlistItemRequestForUser requestForUser, [FromBody] AddWatchlistItemRequestForMovie requestForMovie)
        {
            var result = _watchlistService.Add(requestForUser.UserId, requestForMovie.MovieId);
            return new ObjectResult(result) { StatusCode = (int)result.StatusCode };
        }

        /// <summary>
        /// Gets user watchlist
        /// </summary>
        [ProducesResponseType(typeof(GenericResult<WatchlistItemResponse>), 200)]
        [ProducesResponseType(typeof(GenericResult<WatchlistItemResponse>), 400)]
        [HttpGet("{userId}/watchlist")]
        public IActionResult Get([FromRoute] GetUserWatchlistRequest request)
        {
            var result = _watchlistService.Get(request.UserId);
            return new ObjectResult(result) { StatusCode = (int)result.StatusCode };
        }

        /// <summary>
        /// Marks movie as watched
        /// </summary>
        [ProducesResponseType(204)]
        [ProducesResponseType(typeof(GenericResult<object>), 400)]
        [HttpPatch("{userId}/watchlist/{movieId}/watched")]
        public IActionResult Patch([FromRoute] UpdateWatchlistItemStatusRequest request)
        {
            var result = _watchlistService.MarkAsWatched(request.UserId, request.MovieId);
            return new ObjectResult(result) { StatusCode = (int)result.StatusCode };
        }
    }
}
