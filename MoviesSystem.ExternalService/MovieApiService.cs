using Microsoft.Extensions.Configuration;
using MoviesSystem.ExternalService.Models;
using MoviesSystem.Models.Responses;
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
        private readonly IConfiguration _configuration;
        private readonly string _apiKey;

        public MovieApiService(IHttpClientFactory factory, IConfiguration configuration)
        {
            _client = factory.CreateClient("MoviesApi");
            _configuration = configuration;
            _apiKey = _configuration.GetSection("MoviesApiService")["ApiKey"];
        }

        public async Task<MovieDetailsResponse> GetMovieDetails(string id)
        {
            string options = "Wikipedia";

            var url = string.Format("en/api/Title/{0}/{1}/{2}", _apiKey, id, options);
            var result = new MovieDetailsResponse();

            var response = await _client.GetAsync(url);

            if (response.IsSuccessStatusCode)
            {
                var stringResponse = await response.Content.ReadAsStringAsync();

                result = JsonSerializer.Deserialize<MovieDetailsResponse>(stringResponse,
                    new JsonSerializerOptions() { PropertyNamingPolicy = JsonNamingPolicy.CamelCase });
            }
            else
            {
                throw new HttpRequestException(response.ReasonPhrase);
            }

            return result;
        }

        public async Task<MoviesResponse> GetMovies(string title)
        {
            var url = string.Format("/api/Search/{0}/{1}", _apiKey, title);
            var result = new MoviesResponse();

            var response = await _client.GetAsync(url);

            if (response.IsSuccessStatusCode)
            {
                var stringResponse = await response.Content.ReadAsStringAsync();

                result = JsonSerializer.Deserialize<MoviesResponse>(stringResponse,
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
