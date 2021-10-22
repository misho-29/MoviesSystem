using Microsoft.AspNetCore.Mvc;
using MoviesSystem.ExternalService;
using MoviesSystem.ExternalService.Models.RequestModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MoviesSystem.App.API.Controllers
{
    [ApiController]
    [Route("api/[controller]s")]
    public class MovieController : ControllerBase
    {
        private readonly IMovieApiService _movieApiService;

        public MovieController(IMovieApiService movieApiService)
        {
            _movieApiService = movieApiService;
        }

        [HttpGet]
        public async Task<IActionResult> Get([FromQuery]GetMovieByTitleRequest request)
        {
            var response = await _movieApiService.GetMovies(request.Title);

            return Ok(response.Results);
        }
    }
}
