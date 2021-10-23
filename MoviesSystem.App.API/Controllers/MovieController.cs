using Microsoft.AspNetCore.Mvc;
using MoviesSystem.Domain.Models.Responses;
using MoviesSystem.ExternalService;
using MoviesSystem.ExternalService.Models.Requests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MoviesSystem.App.API.Controllers
{
    [Produces("application/json")]
    [ApiController]
    [Route("api/[controller]s")]
    public class MovieController : ControllerBase
    {
        private readonly IMovieApiService _movieApiService;

        public MovieController(IMovieApiService movieApiService)
        {
            _movieApiService = movieApiService;
        }

        /// <summary>
        /// Finds movies by title
        /// </summary>
        [HttpGet]
        //[ProducesResponseType(typeof(GenericResultType<>), 200)]
        public async Task<IActionResult> Get([FromQuery]GetMovieByTitleRequest request)
        {
            var response = await _movieApiService.GetMovies(request.Title);

            return Ok(response.Results);
        }
    }
}
