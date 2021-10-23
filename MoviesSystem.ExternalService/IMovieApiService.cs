using MoviesSystem.Models.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoviesSystem.ExternalService
{
    public interface IMovieApiService
    {
        Task<MoviesResponse> GetMovies(string title);
        Task<MovieDetailsResponse> GetMovieDetails(string id);
    }
}
