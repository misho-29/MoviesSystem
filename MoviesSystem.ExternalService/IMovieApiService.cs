using MoviesSystem.ExternalService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoviesSystem.ExternalService
{
    public interface IMovieApiService
    {
        Task<GetMoviesResponseModel> GetMovies(string title);
    }
}
