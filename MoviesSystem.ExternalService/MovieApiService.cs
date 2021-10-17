using MoviesSystem.ExternalService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace MoviesSystem.ExternalService
{
    public class MovieApiService : IMovieApiService
    {
        private readonly HttpClient _client;

        public MovieApiService(IHttpClientFactory factory)
        {
            _client = factory.CreateClient("MoviesApi");
        }

        public async Task<GetMoviesResponseModel> GetMovies(string title)
        {
            var url = string.Format("/api/Search/{0}/{1}", "k_ki73yo05", title);
            var result = new GetMoviesResponseModel();

            var response = await _client.GetAsync(url);

            if (response.IsSuccessStatusCode)
            {
                var stringResponse = await response.Content.ReadAsStringAsync();

                result = JsonSerializer.Deserialize<GetMoviesResponseModel>(stringResponse,
                    new JsonSerializerOptions() { PropertyNamingPolicy = JsonNamingPolicy.CamelCase });
            }
            else
            {
                throw new HttpRequestException(response.ReasonPhrase);
            }

            return result;
        }
    }
}
