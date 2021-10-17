using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MoviesSystem.App.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        [HttpPost("{userId}/watchlist")]
        public void Post(string userId, [FromBody] string movieId)
        {
        }

        [HttpGet("{userId}/watchlist")]
        public void Get(string userId)
        {
        }

        [HttpPatch("{userId}/watchlist/{movieId}/watched")]
        public void Patch(int userId, string movieId)
        {
        }
    }
}
