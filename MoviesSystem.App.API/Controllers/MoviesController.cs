using Microsoft.AspNetCore.Mvc;
using MoviesSystem.Domain.Models.Responses;
using MoviesSystem.ExternalService;
using MoviesSystem.ExternalService.Models.Requests;
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
    public class MoviesController : ControllerBase
    {
        private readonly IMoviesApiService _movieApiService;

        public MoviesController(IMoviesApiService movieApiService)
        {
            _movieApiService = movieApiService;
        }

        /// <summary>
        /// Finds movies by title
        /// </summary>
        [HttpGet]
        [ProducesResponseType(typeof(GenericResult<List<Models.Responses.MovieModel>>), 200)]
        [ProducesResponseType(typeof(GenericResult<List<Models.Responses.MovieModel>>), 400)]
        public async Task<IActionResult> Get([FromQuery]GetMovieByTitleRequest request)
        {
            var response = await _movieApiService.GetMovies(request.Title);

            var result = new GenericResult<List<Models.Responses.MovieModel>>(HttpStatusCode.OK, response.Results);

            return new ObjectResult(result) { StatusCode = (int)result.StatusCode };
        }
    }
}
