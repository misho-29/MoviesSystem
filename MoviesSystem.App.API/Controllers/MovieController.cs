using Microsoft.AspNetCore.Mvc;
using MoviesSystem.ExternalService;
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

        [HttpGet("{title}")]
        public async Task<IActionResult> Get(string title)
        {
            var response = await _movieApiService.GetMovies(title);

            return Ok(response.Results);
        }
    }
}
